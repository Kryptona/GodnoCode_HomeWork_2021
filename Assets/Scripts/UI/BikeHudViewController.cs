using UnityEngine;
using UnityEngine.UI;

namespace Race.UI
{
    public class BikeHudViewController : MonoBehaviour
    {
        [SerializeField] private Text labelSpeed;
        [SerializeField] private Text labelDistance;
        [SerializeField] private Text labelRollAngle;
        [SerializeField] private Text labelLapNumber;
        [SerializeField] private Text labelHeat;
        [SerializeField] private Text labelFuel;

        [SerializeField] private Bike bike;

        private void FixedUpdate()
        {
            int velocity = (int) bike.Velocity;
            labelSpeed.text = "Speed: " + velocity + " m/s";

            int distance = (int) bike.Distance;
            labelDistance.text = "Distance: " + distance + " m";

            int roll = (int) (bike.RollAngle) % 360;
            labelRollAngle.text = "Angle: " + roll + " deg";

            int laps = (int) (bike.Velocity / bike.Track.GetTrackLength());
            labelLapNumber.text = "Lap: " + (laps + 1);
            
            int heat = (int) (bike.GetNormalizedHeat() * 100.0f);
            labelHeat.text = "Heat: " + heat;
            
            int fuel = (int) (bike.Fuel);
            labelFuel.text = "Fuel: " + fuel;
        }
    }
}