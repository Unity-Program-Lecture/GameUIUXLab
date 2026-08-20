using UnityEngine;

public class UIScreenFlowController : MonoBehaviour
{
    public enum ScreenState
    {
        Title,
        PlayHud,
        Pause,
        Result
    }

    #region serialized

    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject playHudScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject resultScreen;

    #endregion

    private ScreenState _currentScreenState;

    #region unity event

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() => ChangeScreenState(ScreenState.Title);

    #endregion

    public void ShowTitle() => ChangeScreenState(ScreenState.Title);
    public void ShowPlayHud() => ChangeScreenState(ScreenState.PlayHud);
    public void ShowPause() => ChangeScreenState(ScreenState.Pause);
    public void ShowResult() => ChangeScreenState(ScreenState.Result);
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ChangeScreenState(ScreenState newState)
    {
        _currentScreenState = newState;
        ShowCurrentScreen();
    }

    private void ShowCurrentScreen()
    {
        titleScreen.SetActive(_currentScreenState == ScreenState.Title);
        playHudScreen.SetActive(_currentScreenState == ScreenState.PlayHud);
        pauseScreen.SetActive(_currentScreenState == ScreenState.Pause);
        resultScreen.SetActive(_currentScreenState == ScreenState.Result);
    }
}
