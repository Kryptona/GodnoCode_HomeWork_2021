using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Data model
    /// </summary>
    [System.Serializable]
    public class BikeParameters
    {
        [Range(0.0f, 10.0f)] public float mass;

        [Range(0.0f, 100.0f)] public float thrust;

        [Range(0.0f, 100.0f)] public float agility;
        public float maxSpeed;

        public bool afterburner;

        public GameObject engineModel;
        public GameObject hullModel;
    }

    /// <summary>
    /// Controller
    /// </summary>
    public class Bike : MonoBehaviour
    {
        [SerializeField] private BikeParameters _bikeParametersInit;

        [SerializeField] private BikeViewController m_VisualController;

        /// <summary>
        /// Нормализованное значение, управление газом байка. От -1 до +1
        /// </summary>
        private float _forwardThrustAxis;

        /// <summary>
        /// Устанавливает значение педали газа
        /// </summary>
        /// <param name="value"></param>
        public void SetForwardThrustAxis(float value) => _forwardThrustAxis = value;

        /// <summary>
        ///Управление отклонением влево\вправо
        /// </summary>
        private float _horizontalThrustAxis;

        public void SetHorizontalThrustAxis(float value) => _horizontalThrustAxis = value;

        private void FixedUpdate()
        {
            MoveBike();
        }

        private void MoveBike()
        {
            var currentForwardVelocity = _forwardThrustAxis * _bikeParametersInit.maxSpeed;
            Vector3 forwardMoveDelta = Time.deltaTime * currentForwardVelocity * transform.forward;
            transform.position += forwardMoveDelta;
        }
    }
}