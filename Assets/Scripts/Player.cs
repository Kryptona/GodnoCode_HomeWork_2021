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

        [SerializeField] private Bike activeBike;

        private void FixedUpdate()
        {
            ControlBike();
        }

        private void ControlBike()
        {
            activeBike.SetForwardThrustAxis(0);
            activeBike.SetHorizontalThrustAxis(0);
            
            if (Input.GetKey(KeyCode.W))
            {
                activeBike.SetForwardThrustAxis(1);
            } else if (Input.GetKey(KeyCode.S))
            {
                activeBike.SetForwardThrustAxis(-1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                activeBike.SetHorizontalThrustAxis(1);
            } else if (Input.GetKey(KeyCode.D))
            {
                activeBike.SetHorizontalThrustAxis(-1);
            }
        }
    }
}