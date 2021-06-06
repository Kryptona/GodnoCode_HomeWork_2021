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
        [Range(0.0f, 1.0f)] public float leanerDrag;
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

        [SerializeField] private RaceTrack _track;
        private float _distance;
        private float _velocity;
        private float _rollAngle;


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
            // MoveBike();
            UpdateBikePhysics();
        }

        private void MoveBike()
        {
            var currentForwardVelocity = _forwardThrustAxis * _bikeParametersInit.maxSpeed;
            Vector3 forwardMoveDelta = Time.deltaTime * currentForwardVelocity * transform.forward;
            transform.position += forwardMoveDelta;
        }

        private void UpdateBikePhysics()
        {
            float dt = Time.deltaTime;
            float dv = dt * _forwardThrustAxis * _bikeParametersInit.thrust;
            _velocity += dv;

            _velocity = Mathf.Clamp(_velocity, -_bikeParametersInit.maxSpeed, _bikeParametersInit.maxSpeed);
            _distance += _velocity * dt;

            _velocity += -_velocity * _bikeParametersInit.leanerDrag * dt;

            if (_distance < 0)
                _distance = 0;

            _rollAngle += _bikeParametersInit.agility * dt * _horizontalThrustAxis;

            Vector3 bikePos = _track.GetPosition(_distance);
            Vector3 bikeDir = _track.GetDirection(_distance);

            Quaternion q = Quaternion.AngleAxis(_rollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * _track.Radius);

            transform.position = bikePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(bikeDir, trackOffset);
        }
    }
}