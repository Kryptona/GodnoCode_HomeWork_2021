using System;
using UnityEngine;

namespace Race.UI
{
    public class PauseViewController : MonoBehaviour
    {
        public static readonly string MainMenuScene = "scene_main_menu";

        [SerializeField] private RectTransform _content;

        [SerializeField] private RaceController _raceController;

        private void Start()
        {
            _content.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_raceController.isRaceActive)
                {
                    _content.gameObject.SetActive(!_content.gameObject.activeInHierarchy);

                    UpdateGameActivity(!_content.gameObject.activeInHierarchy);
                }
            }
        }

        private void UpdateGameActivity(bool flag)
        {
            Time.timeScale = flag ? 1 : 0;
        }

        public void OnButtonContinue()
        {
            UpdateGameActivity(true);
            _content.gameObject.SetActive(false);
        }

        public void OnButtonEndRace()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenuScene);
        }
    }
}