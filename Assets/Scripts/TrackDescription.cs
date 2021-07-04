using System.Collections;
using System.Collections.Generic;
using Tracks;
using UnityEngine;

namespace Race
{
    [CreateAssetMenu]
    public class TrackDescription : ScriptableObject
    {
        [SerializeField] private string _trackName;
        public string TrackName => _trackName;

        [SerializeField] private string _sceneNickname;
        public string SceneNickname => _sceneNickname;
        
        [SerializeField] private Sprite _previewImage;
        public Sprite PreviewImage => _previewImage;
        
        [SerializeField] private RaceTrack _raceTrack;
        public RaceTrack RaceTrack => _raceTrack;
        
    }
}