using System.Collections.Generic;
using Tracks;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Race
{
#if UNITY_EDITOR
    [CustomEditor(typeof(RaceTrackCurved))]
    public class RaceTrackCurvedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                (target as RaceTrackCurved)?.GenerateTrackData();
            }
        }
    }
#endif

    public class RaceTrackCurved : RaceTrack
    {
        [SerializeField] private CurvedTrackPoint[] _trackPoints;

        //количество точек между двумя CurvedTrackPoint
        [SerializeField] private int _division;

        [SerializeField] private Quaternion[] _trackSampledRotation;
        [SerializeField] private Vector3[] _trackSampledPoints;

        [SerializeField] private bool _debugDrawBezier;
        [SerializeField] private float[] _trackSampledSegmentLengths;
        [SerializeField] private float _trackSampledLength;

#if UNITY_EDITOR
        public void GenerateTrackData()
        {
            Debug.Log("Generation track buttom");

            if (_trackPoints.Length < 3)
                return;

            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rotations = new List<Quaternion>();


            for (var i = 0; i < _trackPoints.Length - 1; i++)
            {
                var newPoints = GenerateBezierPoints(_trackPoints[i], _trackPoints[i + 1], _division);

                var newRotations = GenerateRotations(_trackPoints[i].transform, _trackPoints[i + 1].transform, newPoints);

                rotations.AddRange(newRotations);
                points.AddRange(newPoints);
            }

            var lastPoint = GenerateBezierPoints(_trackPoints[_trackPoints.Length - 1], _trackPoints[0], _division);
            var lastRotations = GenerateRotations(_trackPoints[_trackPoints.Length - 1].transform, _trackPoints[0].transform, lastPoint);

            rotations.AddRange(lastRotations);
            points.AddRange(lastPoint);

            _trackSampledRotation = rotations.ToArray();
            _trackSampledPoints = points.ToArray();

            //precompute lengths
            _trackSampledSegmentLengths = new float[_trackSampledPoints.Length - 1];

            _trackSampledLength = 0;

            for (var i = 0; i < _trackSampledPoints.Length - 1; i++)
            {
                Vector3 a = _trackSampledPoints[i];
                Vector3 b = _trackSampledPoints[i + 1];

                float segmentLength = (b - a).magnitude;
                _trackSampledSegmentLengths[i] = segmentLength;
                _trackSampledLength += segmentLength;
            }

            // //ЧТобы Unity обновила данные
            EditorUtility.SetDirty(this);
        }

        private void DrawSampledTrackPoint()
        {
            Handles.DrawAAPolyLine(_trackSampledPoints);
        }

        private Quaternion[] GenerateRotations(Transform a, Transform b, Vector3[] points)
        {
            List<Quaternion> rotations = new List<Quaternion>();
            float t = 0;

            for (var i = 0; i < points.Length - 1; i++)
            {
                Vector3 dir = (points[i + 1] - points[i]).normalized;

                Vector3 up = Vector3.Lerp(a.up, b.up, t);

                Quaternion rotation = Quaternion.LookRotation(dir, up);

                rotations.Add(rotation);
                t += 1.0f / (points.Length - 1);
            }

            rotations.Add(b.rotation);

            return rotations.ToArray();
        }

        private Vector3[] GenerateBezierPoints(CurvedTrackPoint a, CurvedTrackPoint b, int division)
        {
            return Handles.MakeBezierPoints(a.transform.position,
                b.transform.position,
                a.transform.position + a.transform.forward * a.GetLength(),
                b.transform.position - b.transform.forward * b.GetLength(),
                division);
        }

        private void OnDrawGizmos()
        {
            if (_debugDrawBezier)
                DrawBezierCurve();
            else
                DrawSampledTrackPoint();
        }

        private void DrawBezierCurve()
        {
            if (_trackPoints.Length < 3)
                return;

            for (var i = 0; i < _trackPoints.Length - 1; i++)
            {
                DrawTrackPartGizmo(_trackPoints[i], _trackPoints[i + 1]);
            }

            DrawTrackPartGizmo(_trackPoints[_trackPoints.Length - 1], _trackPoints[0]);
        }

        private void DrawTrackPartGizmo(CurvedTrackPoint a, CurvedTrackPoint b)
        {
            Handles.DrawBezier(a.transform.position,
                b.transform.position,
                a.transform.position + a.transform.forward * a.GetLength(),
                b.transform.position - b.transform.forward * b.GetLength(),
                Color.green,
                Texture2D.whiteTexture,
                1.0f);
        }
#endif
        public override Vector3 GetDirection(float distance)
        {
            //чтобы сделать значение дистанции цикличным
            distance = Mathf.Repeat(distance, _trackSampledLength);

            for (var i = 0; i < _trackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - _trackSampledSegmentLengths[i];

                if (diff < 0)
                {
                    return (_trackSampledPoints[i + 1] - _trackSampledPoints[i]).normalized;
                }
                else
                    distance -= _trackSampledSegmentLengths[i];
            }

            return Vector3.forward;
        }

        public override Vector3 GetPosition(float distance)
        {
            //чтобы сделать значение дистанции цикличным
            distance = Mathf.Repeat(distance, _trackSampledLength);

            for (var i = 0; i < _trackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - _trackSampledSegmentLengths[i];

                if (diff < 0)
                {
                    float t = distance / _trackSampledSegmentLengths[i];
                    return Vector3.Lerp(_trackSampledPoints[i], _trackSampledPoints[i + 1], t);
                }
                else
                    distance -= _trackSampledSegmentLengths[i];
            }

            return Vector3.zero;
        }

        public override Quaternion GetRotation(float distance)
        {
            distance = Mathf.Repeat(distance, _trackSampledLength);

            for (var i = 0; i < _trackSampledSegmentLengths.Length; i++)
            {
                float diff = distance - _trackSampledSegmentLengths[i];

                if (diff < 0)
                {
                    //return position
                    float t = distance / _trackSampledSegmentLengths[i];

                    return Quaternion.Slerp(_trackSampledRotation[i], _trackSampledRotation[i + 1], t);
                }
                else
                {
                    distance -= _trackSampledSegmentLengths[i];
                }
            }

            return Quaternion.identity;
        }

        public override float GetTrackLength()
        {
            return _trackSampledLength;
        }
    }
}