using System;
using UnityEngine;
using UnityEngine.UI;


namespace Race.UI
{
    public class OptionsViewController : MonoBehaviour
    {
        [SerializeField] private Dropdown _screenResolutionDropdown;
        [SerializeField] private MainMenuViewController _mainMenuController;
        
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonExit()
        {
            _mainMenuController.gameObject.SetActive(true);
            
            gameObject.SetActive(false);
        }

        public void HandleInputData()
        {
            switch (_screenResolutionDropdown.value)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, true);
                    break;
                case 1:
                    Screen.SetResolution(1280, 1024, true);
                    break;
                case 2:
                    Screen.SetResolution(800, 600, true);
                    break;
            }
        }
    }
}
