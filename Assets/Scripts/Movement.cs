using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Race
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private int speed;

        [SerializeField] private Transform[] points;

        private int startIndex = 0;
        private int finishIndex = 1;

        [SerializeField] private RaceTrackLeaner @params;

        [SerializeField] private float distance;
        [SerializeField] private Transform obj;

        /// <summary>
        /// Для валидации данных класса
        /// </summary>
        private void FixedUpdate()
        {
            if (Move(points[startIndex].position, points[finishIndex].position))
            {
                startIndex = (startIndex + 1) % points.Length;
                finishIndex = (finishIndex + 1) % points.Length;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            var prev = points[0];
            for (var i = 1; i < points.Length; i++)
            {
                Gizmos.DrawLine(prev.position, points[i].position);
                prev = points[i];
            }
        }

        private bool Move(Vector3 start, Vector3 finish)
        {
            var length = @params.GetTrackLength(finish, start);
            if (@params.GetTrackLength(obj.position, start) >= length)
            {
                obj.position = finish;
                return true;
            }

            obj.transform.position = Vector3.MoveTowards(obj.transform.position, finish, speed * Time.deltaTime);
            return false;
        }
    }
}