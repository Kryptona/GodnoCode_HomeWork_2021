using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Race
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private int _maxLaps;

        public enum RaceMode
        {
            Laps,
            Time,
            LastStanding
        }

        [SerializeField] private RaceMode _raceMode;

        [SerializeField] private UnityEvent _eventRaceFinish;
        [SerializeField] private UnityEvent _eventRaceStart;

        [SerializeField] private Bike[] _bikes;
        
        [SerializeField] private int _countdownTimer;
        public int CountdownTimer => _countdownTimer;
        
        //Timer
        private float _countTimer;
        public float CountTimer => _countTimer;

        public bool isRaceActive { get; private set; }

        public void StartRace()
        {
            isRaceActive = true;

            _countTimer = _countdownTimer;

        }

        public void EndRace()
        {
            isRaceActive = false;
        }

        private void Start()
        {
            StartRace();
        }

        private void Update()
        {
            if(!isRaceActive)
                return;

            UpdateRacePrestart();
        }

        private void UpdateRacePrestart()
        {
            if (_countTimer > 0)
            {
                _countTimer -= Time.deltaTime;

                if (_countTimer < 0)
                {
                    foreach (var bike in _bikes)
                        bike.isMovementControlsActive = true;
                }
            }
        }
    }
}