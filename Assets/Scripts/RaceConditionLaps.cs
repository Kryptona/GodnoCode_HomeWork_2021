using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class RaceConditionLaps : RaceCondition
    {
        [SerializeField] private RaceController _raceController;

        private void Update()
        {
            if(!_raceController.isRaceActive && isTriggered)
                return;

            Bike[] bikes = _raceController.Bikes;
            
            foreach (var bike in bikes)
            {
                var laps = (int) (bike.Distance / bike.Track.GetTrackLength());

                //добавлено вычисление времени лучшего круга
                if (bike.Statistics.Lap < laps)
                {
                    var currentTime = Time.time;
                    var lapTime = currentTime - bike.Statistics.CurrentLapStartTime;
                    bike.Statistics.CurrentLapStartTime = currentTime;
                    
                    bike.Statistics.Lap = laps;
                    
                    if (bike.Statistics.BestLapTime == null || bike.Statistics.BestLapTime > lapTime)
                        bike.Statistics.BestLapTime = lapTime;
                }
                
                if(laps < _raceController.MaxLaps)
                    return;
            }

            isTriggered = true;
        }
    }
}

