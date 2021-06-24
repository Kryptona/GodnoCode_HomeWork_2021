using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class RaceResultsViewController : MonoBehaviour
    {
        [SerializeField] private Text _place;
        [SerializeField] private Text _topSpeed;
        [SerializeField] private Text _totalTime;
        [SerializeField] private Text _bestLapTime;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(Bike.BikeStatistics statistics)
        {
            gameObject.SetActive(true);
            
            _place.text = "Place: " + statistics.RacePlace;
            _topSpeed.text = "Top speed: " + ((int)statistics.TopSpeed) + " m/s";
            _totalTime.text = "Total time: " + statistics.TotalTime  + " sec";
            _bestLapTime.text = "Best lap time: " + statistics.BestLapTime  + " sec";
        }
    }
}

