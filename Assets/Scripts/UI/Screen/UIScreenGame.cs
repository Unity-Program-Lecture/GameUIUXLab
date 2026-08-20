using UnityEngine;
using UnityEngine.UI;

namespace UI.Screen
{
    public class UIScreenGame : UIScreen
    {
        #region serialized

        [SerializeField] private Button pauseButton;
        [SerializeField] private GameObject pauseScreen;

        #endregion

        #region unity event

        private void Awake() => pauseButton.onClick.AddListener(PauseGame);

        #endregion

        protected override void OnShow() => ResumeGame();

        public void PauseGame()
        {
            pauseButton.gameObject.SetActive(false);
            pauseScreen.SetActive(true);
        }

        public void ResumeGame()
        {
            pauseButton.gameObject.SetActive(true);
            pauseScreen.SetActive(false);
        }
    }
}