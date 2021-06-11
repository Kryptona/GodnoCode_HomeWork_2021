using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private RaceTrack _track;
        [SerializeField] private float _rollAngle;
        [SerializeField] private float _distance;
        [SerializeField, Range(0.0f, 20.0f)] private float _radiusModifier = 1;
        [SerializeField] private int _speedRotation;

        private void FixedUpdate()
        {
            Rotate();
        }

        private void OnValidate()
        {
            SetObstaclePosition();
        }

        private void SetObstaclePosition()
        {
            Vector3 obstaclePos = _track.GetPosition(_distance);
            Vector3 obstacleDir = _track.GetDirection(_distance);

            Quaternion q = Quaternion.AngleAxis(_rollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * (_radiusModifier * _track.Radius));

            transform.position = obstaclePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Vector3 centerLinePos = _track.GetPosition(_distance);
            Gizmos.DrawWireSphere(centerLinePos, _track.Radius);
        }

        /// <summary>
        /// Вращение вокруг оси трека
        /// </summary>
        private void Rotate()
        {
            transform.RotateAround(_track.GetPosition(_distance), Vector3.forward, _speedRotation * Time.deltaTime);
        }
    }
}
