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
        [Header("Leaner track properties")] [SerializeField]
        private Transform m_Start;

        [SerializeField] private Transform m_End;

        public override Vector3 GetPosition(float distance)
        {
            //Clamp - принимает значение, min
            // distance = Mathf.Clamp(distance, 0, GetTrackLength());

            Vector3 direction = m_End.position - m_Start.position;
            
            // if ((m_Start.position + distance * direction.normalized).magnitude > direction.magnitude)
            if (distance > direction.magnitude)
            {
                m_TestDistance = m_Start.position.magnitude;
                return m_Start.position;
            }

            if (distance < 0)
            {
                m_TestDistance = m_End.position.magnitude;
                return m_End.position;
            }
            
            return m_Start.position + direction.normalized * distance;
        }

        public override Vector3 GetDirection(float distance)
        {
            return (m_End.position - m_Start.position).normalized;
        }

        public override float GetTrackLength()
        {
            Vector3 direction = m_End.position - m_Start.position;
            return direction.magnitude;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(m_Start.position, m_End.position);
        }

        #region Test

        [SerializeField] private float m_TestDistance;
        [SerializeField] private Transform m_TestObject;

        /// <summary>
        /// Для валидации данных класса
        /// </summary>
        private void OnValidate()
        {
            m_TestObject.position = GetPosition(m_TestDistance);
            m_TestObject.forward = GetDirection(m_TestDistance);
        }

        #endregion
    }
}