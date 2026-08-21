using UnityEngine;
using UnityEngine.UI;

namespace UI.Screen
{
    using Input;

    public class UIScreenGame : UIScreen
    {
        #region serialized

        [SerializeField] private Button pauseButton;
        [SerializeField] private GameObject pauseScreen;

        #endregion

        #region unity event

        private void Awake() => pauseButton.onClick.AddListener(PauseGame);

        #endregion

        protected override void OnShow()
        {
            InputByDevice.Instance.OnPauseGameEvent.AddListener(PauseOrResumeGame);

            ResumeGame();
        }

        protected override void OnHide() => InputByDevice.Instance.OnPauseGameEvent.RemoveListener(PauseOrResumeGame);

        public void PauseGame()
        {
            pauseButton.gameObject.SetActive(false);
            pauseScreen.SetActive(true);

            InputByDevice.Instance.SetInputMode(InputMode.UI);

            if (firstSelectedGameObject)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstSelectedGameObject);
            }
        }

        public void ResumeGame()
        {
            pauseButton.gameObject.SetActive(true);
            pauseScreen.SetActive(false);

            InputByDevice.Instance.SetInputMode(InputMode.Player);
        }

        public void PauseOrResumeGame(bool isPaused)
        {
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
}