using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class EngineSfxController : MonoBehaviour
    {
        [SerializeField] private AudioSource _engineSource;

        [SerializeField] private Bike _bike;

        [Range(0.0f, 1.0f)] [SerializeField] private float _velocityPetchModifier;

        private void Update()
        {
            UpdateEngineSoundSimple();
        }

        private void UpdateEngineSoundSimple()
        {
            _engineSource.pitch = 1.0f + _velocityPetchModifier * _bike.GetNormalizedSpeed();
        }
    }
}