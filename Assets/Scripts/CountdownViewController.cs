using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class CountdownViewController : MonoBehaviour
    {
        [SerializeField] private RaceController _raceController;
        [SerializeField] private Text _label;

        private bool isGo = true;

        private void Update()
        {
            var text = (int) _raceController.CountTimer;
            if (text != 0)
            {
                _label.text = text.ToString();
            }
            else
            {
                if(isGo)
                {
                    _label.text = "GO!";
                    isGo = false;
                    Invoke("DisableText", 1.5f);
                }
            }
        }

        private void DisableText()
        {
            _label.text = "";
            gameObject.SetActive(false);
        }
    }
}