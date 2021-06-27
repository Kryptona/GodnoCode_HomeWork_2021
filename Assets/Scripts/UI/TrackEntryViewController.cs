using UnityEngine;
using UnityEngine.UI;

namespace Race.UI
{
    /// <summary>
    /// Логика выбора нужного трека
    /// </summary>
    public class TrackEntryViewController : MonoBehaviour
    {
        [SerializeField] private TrackDescription _trackDescription;
        [SerializeField] private Text _trackName;

        private TrackDescription _activeDescription;

        private void Start()
        {
            if(_trackDescription != null)
                SetViewValues(_trackDescription);
        }

        public void SetViewValues(TrackDescription description)
        {
            _activeDescription = description;
            
            _trackName.text = description.TrackName;
        }
        public void OnButtonStartLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_activeDescription.SceneNickname);
        }
    }
}

