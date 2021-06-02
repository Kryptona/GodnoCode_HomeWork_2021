using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Базовый класс, который определяет трубу для гонок
    /// </summary>
    public abstract class RaceTrack : MonoBehaviour
    {
        /// <summary>
        /// Радиус трубы
        /// </summary>
        [Header("Base track properties")]
        [SerializeField] private float m_Radius;

        public float Radius => m_Radius;

        /// <summary>
        /// Возвращает длину трека
        /// </summary>
        /// <returns></returns>
        public abstract float GetTrackLength(Vector3 m_End, Vector3 m_Start);

        /// <summary>
        /// Вовзращает позицию в 3Д кривой центр-линии трубы
        /// </summary>
        /// <param name="distance"></param> дистанция от начала трубы до ее GetTrackLength
        /// <returns></returns>
        public abstract Vector3 GetPosition(float distance, Vector3 end, Vector3 start);

        /// <summary>
        /// Возвращает  направление в 3Д кривой центр-линии трубы
        /// Касательная к кривой в точке
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public abstract Vector3 GetDirection(float distance, Vector3 end, Vector3 start);
    }
}