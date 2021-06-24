using System;
using System.Collections;
using System.Collections.Generic;
using Tracks;
using UnityEngine;
using UnityEngine.Events;

namespace Race
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private RaceTrack _track;
        
        [SerializeField] private int _maxLaps;
        public int MaxLaps => _maxLaps;

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
        public Bike[] Bikes => _bikes;

        [SerializeField] private int _countdownTimer;
        public int CountdownTimer => _countdownTimer;

        //Timer
        private float _countTimer;
        public float CountTimer => _countTimer;

        public bool isRaceActive { get; private set; }

        [SerializeField] private RaceCondition[] _conditions;

        public void StartRace()
        {
            _activeBikes = new List<Bike>(_bikes);
            _finishedBikes = new List<Bike>();
            
            isRaceActive = true;

            _countTimer = _countdownTimer;

            foreach (var condition in _conditions)
                condition.OnRaceStart();

            foreach (var bike in _bikes)
                bike.OnRaceStart();

            _eventRaceStart?.Invoke();
        }

        public void EndRace()
        {
            isRaceActive = false;

            foreach (var condition in _conditions)
                condition.OnRaceEnd();
        }

        private void Start()
        {
            StartRace();
        }

        private void Update()
        {
            if (!isRaceActive)
                return;

            UpdateBikeRacePositions();
            UpdateRacePrestart();
            UpdateConditions();
        }

        private void UpdateRacePrestart()
        {
            if (_countTimer > 0)
            {
                _countTimer -= Time.deltaTime;

                if (_countTimer <= 0)
                {
                    foreach (var bike in _bikes)
                        bike.isMovementControlsActive = true;
                }
            }
        }

        private void UpdateConditions()
        {
            if (isRaceActive)
                return;

            foreach (var condition in _conditions)
            {
                if (!condition.isTriggered)
                    return;

                EndRace();

                _eventRaceFinish?.Invoke();
            }
        }

        private List<Bike> _activeBikes;
        private List<Bike> _finishedBikes;

        [SerializeField] private RaceResultsViewController _raceResultsViewController;
        
        //приехал ли байк до конца трека
        private void UpdateBikeRacePositions()
        {
            foreach (var bike in _activeBikes)
            {
                if(_finishedBikes.Contains(bike))
                    continue;
                
                float dist = bike.Distance;
                float totalRaceDistance = _maxLaps * _track.GetTrackLength();

                if (dist > totalRaceDistance)
                {
                    _finishedBikes.Add(bike);
                    bike.Statistics.RacePlace = _finishedBikes.Count;
                    bike.OnRaceEnd();
                    //todo удалить из гонки или выключить управление

                    if (bike.IsPlayerBike)
                    {
                        _raceResultsViewController.Show(bike.Statistics);
                    }
                }
            }
        }
    }
}