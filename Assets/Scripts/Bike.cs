using System;
using System.Collections;
using System.Collections.Generic;
using Tracks;
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

        public float afterBurnerCoolSpeed; // per second
        public float afterBurnerHeatGeneration; // per second
        public float afterBurnerMaxHeat; // per second
        

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
        public static readonly string Tag = "Bike";

        [SerializeField] private BikeParameters _bikeParametersInit;

        [SerializeField] private BikeViewController m_VisualController;

        [SerializeField] private RaceTrack _track;

        //перегрев
        private float _afterBurnerHeat;

        private float _distance;
        private float _velocity;
        private float _rollAngle;
        private float _rollVelocity;

        public float Distance => _distance;
        public float Velocity => _velocity;
        public float RollAngle => _rollAngle;

        private float _prevDistance;
        public float PrevDistance => _prevDistance;

        public RaceTrack Track => _track;

        //топливо
        private float _fuel;
        public float Fuel => _fuel;

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

        /// <summary>
        /// Дополнительный ускоритель
        /// </summary>
        public bool EnableAfterBurner { get; set; }

        public void SetHorizontalThrustAxis(float value) => _horizontalThrustAxis = value;

        private void FixedUpdate()
        {
            UpdateBikePhysics();
            UpdateAfterBurnerHeat();
        }

        public void CoolAfterBurner() => _afterBurnerHeat = 0;

        /// <summary>
        /// Остывание
        /// </summary>
        private void UpdateAfterBurnerHeat()
        {
            _afterBurnerHeat -= _bikeParametersInit.afterBurnerCoolSpeed * Time.deltaTime;

            if (_afterBurnerHeat < 0)
                _afterBurnerHeat = 0;
        }

        public float GetNormalizedHeat()
        {
            if (_bikeParametersInit.afterBurnerMaxHeat > 0)
                return _afterBurnerHeat / _bikeParametersInit.afterBurnerMaxHeat;

            return 0;
        }

        private void UpdateBikePhysics()
        {
            float dt = Time.deltaTime;
            float dS = _velocity * dt;

            //collision with obstacle
            if (Physics.Raycast(transform.position, transform.forward, dS))
            {
                _velocity = -_velocity * _bikeParametersInit.collisionBounceFactor;
                dS = _velocity * dt;
                AddAfterBurnerByObstacle();
            }

            CalcRollVelocity();
            CalcAfterBurner();

            _prevDistance = _distance;
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
            float dt = Time.deltaTime;
            float fthrustMax = _bikeParametersInit.thrust;
            float vMax = _bikeParametersInit.maxSpeed;
            float f = _forwardThrustAxis * _bikeParametersInit.thrust;
            if (EnableAfterBurner && ConsumeFuelForAfterBurner(1.0f * Time.deltaTime))
            {
                _afterBurnerHeat += _bikeParametersInit.afterBurnerHeatGeneration * Time.deltaTime;

                f += _bikeParametersInit.afterBurnerThrust;
                vMax += _bikeParametersInit.afterburnerMaxSpeedBonus;
                fthrustMax += _bikeParametersInit.afterBurnerThrust;
            }

            f += -_velocity * (fthrustMax / vMax);
            _velocity += dt * f;
        }

        /// <summary>
        /// Снижает скорость при прохождении через замедляющий паверап
        /// </summary>
        /// <param name="velocityDebuff">Часть от скорости, на которую основная скорость будет снижена</param>
        public void ToBrakeByPowerup(float velocityDebuff)
        {
            if (_velocity > 0)
                _velocity -= _velocity * velocityDebuff;
        }

        /// <summary>
        /// Делает перегрев максимальным, если было столкновение с препятствием
        /// </summary>
        private void AddAfterBurnerByObstacle()
        {
            _afterBurnerHeat = _bikeParametersInit.afterBurnerMaxHeat;
        }

        public void CalcRollVelocity()
        {
            float dt = Time.deltaTime;
            _rollVelocity += dt * _horizontalThrustAxis * _bikeParametersInit.agility;
            _rollVelocity = Mathf.Clamp(_rollVelocity, -_bikeParametersInit.maxSpeedRotation, _bikeParametersInit.maxSpeedRotation);
            _rollVelocity += -_rollVelocity * _bikeParametersInit.leanerDrag * dt;
            _rollAngle += _rollVelocity * dt;
        }

        public void AddFuel(float amount)
        {
            _fuel += amount;
            _fuel = Mathf.Clamp(_fuel, 0, 100);
        }

        public bool ConsumeFuelForAfterBurner(float amount)
        {
            if (_fuel < amount)
                return false;

            _fuel -= amount;

            return true;
        }
    }
}