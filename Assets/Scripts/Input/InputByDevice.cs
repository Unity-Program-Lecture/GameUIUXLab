using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace Input
{
    public enum InputMode
    {
        Player = 0,
        UI,
    }

    public class InputByDevice : UnitySingleton<InputByDevice>
    {
        #region serialized

        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private string playerInputActionMapName = "Player";
        [SerializeField] private string uiInputActionMapName = "UI";
        [SerializeField] private UnityEvent<bool> onPauseGame;

        #endregion

        private InputActionMap _playerActionMap;
        private InputActionMap _uiActionMap;

        #region Player Input Actions

        private InputAction _pauseAction;

        #endregion

        #region UI Input Actions

        private InputAction _resumeAction;

        #endregion

        public UnityEvent<bool> OnPauseGameEvent => onPauseGame ??= new UnityEvent<bool>();

        #region unity event

        protected override void OnInitialize()
        {
            if (!inputActionAsset)
            {
                Debug.LogError("InputActionAsset is not assigned.", this);
                return;
            }

            _playerActionMap = inputActionAsset.FindActionMap(playerInputActionMapName);
            if (_playerActionMap is null)
            {
                Debug.LogError($"Player InputActionMap '{playerInputActionMapName}' not found in InputActionAsset.", this);
                return;
            }

            _uiActionMap = inputActionAsset.FindActionMap(uiInputActionMapName);
            if (_uiActionMap is null)
            {
                Debug.LogError($"UI InputActionMap '{uiInputActionMapName}' not found in InputActionAsset.", this);
                return;
            }

            _pauseAction = _playerActionMap.FindAction("Pause");
            if (_pauseAction is null)
            {
                Debug.LogError($"Pause InputAction not found in Player InputActionMap.", this);
                return;
            }

            _resumeAction = _uiActionMap.FindAction("Resume");
            if (_resumeAction is null)
            {
                Debug.LogError($"Resume InputAction not found in UI InputActionMap.", this);
                return;
            }

            _pauseAction.performed += OnPauseGame;
            _resumeAction.performed += OnResumeGame;
        }

        protected override void OnDispose()
        {
            if (_pauseAction is not null)
            {
                _pauseAction.performed -= OnPauseGame;
            }

            if (_resumeAction is not null)
            {
                _resumeAction.performed -= OnResumeGame;
            }
        }

        #endregion

        public void SetInputMode(InputMode mode)
        {
            switch (mode)
            {
                case InputMode.Player:
                    _playerActionMap?.Enable();
                    _uiActionMap?.Disable();
                    break;
                case InputMode.UI:
                    _playerActionMap?.Disable();
                    _uiActionMap?.Enable();
                    break;
                default:
                    Debug.LogError($"Invalid InputMode: {mode}", this);
                    break;
            }
        }

        private void OnPauseGame(InputAction.CallbackContext context) => onPauseGame?.Invoke(true);
        private void OnResumeGame(InputAction.CallbackContext context) => onPauseGame?.Invoke(false);
    }
}