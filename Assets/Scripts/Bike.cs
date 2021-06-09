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

        public float afterBurnerThrust;

        [Range(0.0f, 100.0f)] public float agility;
        [Range(0.0f, 1.0f)] public float leanerDrag;

        public float afterburnerMaxSpeedBonus;

        [Range(0.0f, 1.0f)] public float collisionBounceFactor;

        public float maxSpeed;
        public float maxSpeedRotation;

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
        private float _rollVelocity;
        public float RollVelocity => _rollVelocity;

        public float Distance => _distance;
        public float Velocity => _velocity;
        public float RollAngle => _rollAngle;

        public RaceTrack Track => _track;

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

        public float HorizontalThrustAxis => _horizontalThrustAxis;

        /// <summary>
        /// Дополнительный ускоритель
        /// </summary>
        public bool EnableAfterBurner { get; set; }

        public void SetHorizontalThrustAxis(float value) => _horizontalThrustAxis = value;

        private void FixedUpdate()
        {
            UpdateBikePhysics();
        }

        private void UpdateBikePhysics()
        {
            float dt = Time.deltaTime;
            
            // работа ускорителя
            float fthrustMax = _bikeParametersInit.thrust;
            float vMax = _bikeParametersInit.maxSpeed;
            float f = _forwardThrustAxis * _bikeParametersInit.thrust;
            if (EnableAfterBurner)
            {
                f += _bikeParametersInit.afterBurnerThrust;
                vMax += _bikeParametersInit.afterburnerMaxSpeedBonus;
                fthrustMax += _bikeParametersInit.afterBurnerThrust;
            }

            f += -_velocity * (fthrustMax / vMax);
            _velocity += dt * f;


            float dS = _velocity * dt;

            //collision
            if (Physics.Raycast(transform.position, transform.forward, dS))
            {
                _velocity = -_velocity * _bikeParametersInit.collisionBounceFactor;
                dS = _velocity * dt;
            }

            CalcRollVelocity();

            _distance += dS;

            if (_distance < 0)
                _distance = 0;

            Vector3 bikePos = _track.GetPosition(_distance);
            Vector3 bikeDir = _track.GetDirection(_distance);

            Quaternion q = Quaternion.AngleAxis(_rollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * _track.Radius);

            transform.position = bikePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(bikeDir, trackOffset);
        }

        private void CalcAfterBurner()
        {
            
        }
        
        public void CalcRollVelocity()
        {
            float dt = Time.deltaTime;
            _rollVelocity += dt * _horizontalThrustAxis * _bikeParametersInit.agility;
            _rollVelocity = Mathf.Clamp(_rollVelocity, -_bikeParametersInit.maxSpeedRotation, _bikeParametersInit.maxSpeedRotation);
            _rollVelocity += -_rollVelocity * _bikeParametersInit.leanerDrag * dt;
            _rollAngle += _rollVelocity * dt;
        }
    }
}