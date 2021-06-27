using UnityEngine;

namespace Race
{
    public class CurvedTrackPoint : MonoBehaviour
    {
        [SerializeField] private float _length = 1.0f;
        public float GetLength() => _length;
        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.cyan;
        //     Gizmos.DrawSphere(transform.position, 10.0f);
        //     
        // }
    }
}