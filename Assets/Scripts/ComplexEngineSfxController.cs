using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class ComplexEngineSfxController : MonoBehaviour
    {
        [SerializeField] private Bike _bike;
        [SerializeField] private AudioSource _sfxLow;
        [SerializeField] private AudioSource _sfxHigh;
        [SerializeField] private AudioSource _sfxLoud;

        [SerializeField] private AnimationCurve _curveLow; // 0
        [SerializeField] private AnimationCurve _curveHigh; // 0.5
        [SerializeField] private AnimationCurve _curveLoud; // 1
        
        [SerializeField] private AudioSource _sfxSonicBoom;
        [SerializeField] private AnimationCurve _sonicCurve;
        

        public const float PitchFactor = 0.3f;

        [SerializeField] private float _superSonicSpeed;
        
        public bool IsSuperSonic { get; private set; }

        public void SetSuperSonic(bool flag)
        {
            if (!IsSuperSonic && flag)
            {
                _sfxSonicBoom.Play();
            }
            IsSuperSonic = flag;
        }
        
        private void Update()
        {
            SetSuperSonic(Math.Abs(_bike.Velocity) > _superSonicSpeed);
            if (_sfxSonicBoom.isPlaying)
            {
                var t = Mathf.Clamp01(_sfxSonicBoom.time / _sfxSonicBoom.clip.length);

                _sfxSonicBoom.volume = _sonicCurve.Evaluate(t);
            }
            UpdateDynamicEngineSound();
        }

        private void UpdateDynamicEngineSound()
        {
            if (IsSuperSonic)
            {
                _sfxLow.volume = 0;
                _sfxHigh.volume = 0;
                _sfxLoud.volume = 0;
            }
            
            var t = Mathf.Clamp01(_bike.Velocity / _superSonicSpeed);

            _sfxLow.volume = _curveLow.Evaluate(t);

            _sfxLow.pitch = 1.0f + PitchFactor * t;
            
            _sfxHigh.volume = _curveHigh.Evaluate(t);
            
            _sfxHigh.pitch = 1.0f + PitchFactor * t;
            
            _sfxLoud.volume = _curveLoud.Evaluate(t);
        }
        
    }
}

