using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Класс линейного трека
    /// Реализовать замкнутый линейный трек
    /// Реализовать вспомогательный скрипт, который в плеймоде будет двигать по треку объект с настраиваемой скоростью
    /// </summary>
    public class RaceTrackLeaner : RaceTrack
    {
        [Header("Leaner track properties"), SerializeField]
        private Transform _start;

        [SerializeField] private Transform _end;

        public override Vector3 GetPosition(float distance)
        {
            Vector3 direction = _end.position - _start.position;
     
            return _start.position + direction.normalized * distance;
        }

        public override Vector3 GetDirection(float distance)
        {
            return (_end.position - _start.position).normalized;
        }

        public override float GetTrackLength()
        {
            Vector3 direction = _end.position - _start.position;
            return direction.magnitude;
        }
    }
}