using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : UIScreen
{
    #region serialized

    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pauseScreen;

    #endregion

    private void Awake() => pauseButton.onClick.AddListener(() =>
    {
        pauseButton.gameObject.SetActive(false);
        pauseScreen.SetActive(true);
    });

    protected override void OnShow() => ResumeGame();

    public void ResumeGame()
    {
        pauseButton.gameObject.SetActive(true);
        pauseScreen.SetActive(false);
    }
}