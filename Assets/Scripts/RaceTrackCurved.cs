using System;
using System.Collections;
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

        //
        [SerializeField] private Vector3[] _trackSampledPoints;

        [SerializeField] private bool _debugDrawBezier;

        public void GenerateTrackData()
        {
            Debug.Log("Generation track buttom");

            if (_trackPoints.Length < 3)
                return;

            List<Vector3> points = new List<Vector3>();

            for (var i = 0; i < _trackPoints.Length - 1; i++)
                points.AddRange(GenerateBezierPoints(_trackPoints[i], _trackPoints[i + 1], _division));

            points.AddRange(GenerateBezierPoints(_trackPoints[_trackPoints.Length - 1], _trackPoints[0], _division));
            _trackSampledPoints = points.ToArray();

            //ЧТобы Unity обновила данные
            EditorUtility.SetDirty(this);
        }

        private void DrawSampledTrackPoint()
        {
            Handles.DrawAAPolyLine(_trackSampledPoints);
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

        public override float GetTrackLength()
        {
            return 1.0f;
        }

        public override Vector3 GetPosition(float distance)
        {
            return Vector3.zero;
        }

        public override Vector3 GetDirection(float distance)
        {
            return Vector3.zero;
        }
    }
}