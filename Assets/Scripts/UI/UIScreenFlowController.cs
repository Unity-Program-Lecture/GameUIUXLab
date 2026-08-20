using UnityEngine;

public class UIScreenFlowController : MonoBehaviour
{
    public enum ScreenState
    {
        Title = 0,
        PlayHud,
        Result,

        Max
    }

    #region serialized

    [SerializeField] private UIScreen titleScreen;
    [SerializeField] private UIScreen playHudScreen;
    [SerializeField] private UIScreen resultScreen;

    #endregion

    private ScreenState _currentScreenState = ScreenState.Max;
    private UIScreen[] _uIScreens;

    private bool IsCurrentScreenStateValid => IsScreenStateValid(_currentScreenState);

    #region unity event

    private void Awake() => _uIScreens = new UIScreen[] { titleScreen, playHudScreen, resultScreen };

    private void Start()
    {
        foreach (UIScreen screen in _uIScreens)
        {
            screen.Hide();
        }

        ChangeScreenState(ScreenState.Title);
    }

    #endregion

    public void ShowTitle() => ChangeScreenState(ScreenState.Title);
    public void ShowPlayHud() => ChangeScreenState(ScreenState.PlayHud);
    public void ShowResult() => ChangeScreenState(ScreenState.Result);
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private bool IsScreenStateValid(ScreenState state) => state >= ScreenState.Title && state < ScreenState.Max;

    private void ChangeScreenState(ScreenState newState)
    {
        if (!IsScreenStateValid(newState))
        {
            Debug.LogError($"Invalid screen state: {newState}");
            return;
        }

        if (IsCurrentScreenStateValid)
        {
            if (_currentScreenState == newState)
            {
                return;
            }

            _uIScreens[(int)_currentScreenState].Hide();
        }

        _currentScreenState = newState;

        _uIScreens[(int)_currentScreenState].Show();
    }
}
