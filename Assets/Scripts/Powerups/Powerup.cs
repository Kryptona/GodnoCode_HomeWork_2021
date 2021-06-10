using Race;
using Tracks;
using UnityEngine;

namespace Powerups
{
    public abstract class Powerup : MonoBehaviour
    {
        [SerializeField] private RaceTrack _track;
        [SerializeField] private float _rollAngle;
        [SerializeField] private float _distance;

        private void OnValidate()
        {
            SetPowerPosition();
        }
        
        private void SetPowerPosition()
        {
            Vector3 obstaclePos = _track.GetPosition(_distance);
            Vector3 obstacleDir = _track.GetDirection(_distance);

            Quaternion q = Quaternion.AngleAxis(_rollAngle, Vector3.forward);
            Vector3 trackOffset = q * (Vector3.up * (0));

            transform.position = obstaclePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);
        }

        private void FixedUpdate()
        {
            UpdateBikes();
        }

        private void UpdateBikes()
        {
            foreach (var bikeObj in GameObject.FindGameObjectsWithTag(Bike.Tag))
            {
                Bike bike = bikeObj.GetComponent<Bike>();
                float prev = bike.PrevDistance;
                float curr = bike.Distance;

                if (prev < _distance && curr > _distance)
                {
                    OnPickedByBike(bike);
                }

            }
        }

        public abstract void OnPickedByBike(Bike bike);
    }
}