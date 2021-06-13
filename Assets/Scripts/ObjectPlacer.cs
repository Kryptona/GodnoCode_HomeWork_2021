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

        //todo сделать префаб типа колечка и разместить его на треке

        private void Start()
        {
            float distance = 0;
            for (var i = 0; i < _numObjects; i++)
            {
                var e = Instantiate(_prefab);

                e.transform.position = _track.GetPosition(distance);
                e.transform.forward = _track.GetDirection(distance);

                distance += _track.GetTrackLength() / _numObjects;
                
            }
        }
    }
}

