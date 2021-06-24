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
                
                if(laps < _raceController.MaxLaps)
                    return;
            }

            isTriggered = true;
        }
    }
}

