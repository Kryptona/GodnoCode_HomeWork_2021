using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Класс, реализующий движение объекта
    /// </summary>
    public class Movement : MonoBehaviour
    {
        /// <summary>
        /// Скорость объекта
        /// </summary>
        [SerializeField] private int speed;

        /// <summary>
        /// Массив объектов, которые выполняют роль траектории
        /// </summary>
        [SerializeField] private Transform[] points;

        /// <summary>
        /// Начальный индекс в points[] (для отрезка)
        /// </summary>
        private int startIndex = 0;

        /// <summary>
        /// Конечный индекс в points[] (для отрезка)
        /// </summary>
        private int finishIndex = 1;

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
                if (finishIndex == 0) {
                    startIndex = 0;
                    finishIndex = 1;
                    obj.transform.position = points[0].position;
                }
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

        /// <summary>
        /// Для движения объекта
        /// </summary>
        /// <param name="start"></param> Начальное положение объекта
        /// <param name="finish"></param> Конечное положение
        /// <returns></returns> Достиг ли точки назначения
        private bool Move(Vector3 start, Vector3 finish)
        {
            var length = GetSectionLength(finish, start);
            if (GetSectionLength(obj.position, start) >= length)
            {
                obj.position = finish;
                return true;
            }

            obj.transform.position = Vector3.MoveTowards(obj.transform.position, finish, speed * Time.deltaTime);
            return false;
        }

        /// <summary>
        /// Возвращает длину отрезка
        /// </summary>
        /// <param name="m_End"></param>
        /// <param name="m_Start"></param>
        /// <returns></returns>
        private float GetSectionLength(Vector3 m_End, Vector3 m_Start)
        {
            Vector3 direction = m_End - m_Start;
            return direction.magnitude;
        }
    }
}