using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Race
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SplineMeshProxyArr))]
    public class SplineMeshProxyEditorArr : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update"))
            {
                (target as SplineMeshProxyArr).UpdatePoints();
            }
        }
    }
#endif

    [RequireComponent(typeof(SplineMesh.Spline))]
    public class SplineMeshProxyArr : MonoBehaviour
    {
        [SerializeField] private RaceTrackCurved _curvedTrack;

        [SerializeField] private CurvedTrackPoint[] _points;

        public void UpdatePoints()
        {
            var spline = GetComponent<SplineMesh.Spline>();

            for (var i = 0; i < _points.Length ; i++)
            {
                var n0 = spline.nodes[i];
                n0.Position = _points[i].transform.position;
                n0.Direction = _points[i].transform.position + _points[i].transform.forward * _points[i].GetLength();
            }

            var lastNode = spline.nodes.Last();
            lastNode.Position = _points[0].transform.position;
            lastNode.Direction = _points[0].transform.position + _points[0].transform.forward * _points[0].GetLength();
            
        }
    }
}