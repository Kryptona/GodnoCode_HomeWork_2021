using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Игрок гонки
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private string nickName;
        public string Nickname => nickName;

        [SerializeField] private Bike _activeBike;

        private void FixedUpdate()
        {
            ControlBike();
        }

        private void ControlBike()
        {
            _activeBike.SetForwardThrustAxis(0);
            _activeBike.SetHorizontalThrustAxis(0);
            
            if(!_activeBike.isMovementControlsActive)
                return;
            
            if (Input.GetKey(KeyCode.W))
            {
                _activeBike.SetForwardThrustAxis(1);
            } else if (Input.GetKey(KeyCode.S))
            {
                _activeBike.SetForwardThrustAxis(-1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _activeBike.SetHorizontalThrustAxis(1);
            } else if (Input.GetKey(KeyCode.A))
            {
                _activeBike.SetHorizontalThrustAxis(-1);
            }

            _activeBike.EnableAfterBurner = Input.GetKey(KeyCode.Space);
        }
    }
}