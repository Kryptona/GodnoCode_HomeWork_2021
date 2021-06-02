using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Race
{
    /// <summary>
    /// Класс линейного трека
    /// Реализовать замкнутый линейный трек
    /// Реализовать вспомогательный скрипт, который в плеймоде будет двигать по треку объект с настраиваемой скоростью
    /// </summary>
    public class RaceTrackLeaner : RaceTrack
    {
        public override Vector3 GetPosition(float distance, Vector3 m_End, Vector3 m_Start)
        {
            Vector3 direction = m_End - m_Start;

            if (distance > direction.magnitude)
            {
                distance = m_Start.magnitude;
                return m_Start;
            }

            if (distance < 0)
            {
                distance = m_End.magnitude;
                return m_End;
            }

            return m_Start + direction.normalized * distance;
        }

        public override Vector3 GetDirection(float distance, Vector3 m_End, Vector3 m_Start)
        {
            return (m_End - m_Start).normalized;
        }

        public override float GetTrackLength(Vector3 m_End, Vector3 m_Start)
        {
            Vector3 direction = m_End - m_Start;
            return direction.magnitude;
        }
    }
}