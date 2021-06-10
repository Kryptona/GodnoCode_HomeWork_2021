using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class PowerupFuel : Powerup
    {
        [Range(0.0f, 100.0f), SerializeField] private float _fuelAmount;

        public override void OnPickedByBike(Bike bike)
        {
            bike.AddFuel(_fuelAmount);
        }
    }
}