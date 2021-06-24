using System;
using System.Collections;
using System.Collections.Generic;
using Tracks;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Race
{
    //Будет выставлять префабы вдоль трека
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _numObjects;
        [SerializeField] private RaceTrack _track;
        [SerializeField] private bool _randomizeRotation;
        /// <summary>
        /// Шанс выпадения препятствия
        /// </summary>
        [SerializeField] private bool _randomizeExists;
        [SerializeField] private int _randomSeed;

        private void Start()
        {
            var random = new System.Random(_randomSeed);
            float distance = 0;
            for (var i = 0; i < _numObjects; i++)
            {
                if (!_randomizeExists || random.NextDouble() < 0.5)
                {
                    var prefab = Instantiate(_prefab);

                    prefab.transform.position = _track.GetPosition(distance);
                    prefab.transform.rotation = _track.GetRotation(distance);

                    if (_randomizeRotation)
                    {
                        prefab.transform.Rotate(Vector3.forward, random.Next(0, 360), Space.Self);
                    }
                }

                distance += _track.GetTrackLength() / _numObjects;
                
            }
        }
    }
}

