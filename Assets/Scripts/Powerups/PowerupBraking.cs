using Race;
using UnityEngine;

namespace Powerups
{
    //логика для торможения об объект
    public class PowerupBraking : Powerup
    {
        [Range(0.0f, 1.0f), Header("Часть от скорости")]
        public float velocityPartDebuff;

        public override void OnPickedByBike(Bike bike)
        {
            bike.ToBrakeByPowerup(velocityPartDebuff);
        }
    }
}