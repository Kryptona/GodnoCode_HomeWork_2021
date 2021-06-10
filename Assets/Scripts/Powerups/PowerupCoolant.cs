using Race;
using UnityEngine;

namespace Powerups
{
    public class PowerupCoolant : Powerup
    {
        public override void OnPickedByBike(Bike bike)
        {
            bike.CoolAfterBurner();
            Debug.Log("PowerupCoolant " + bike.name);
        }
    }
}