using UnityEngine;

namespace UI.Screen
{
    public class UIScreen : MonoBehaviour
    {
        [SerializeField] protected GameObject firstSelectedGameObject;

        public void Show()
        {
            gameObject.SetActive(true);

            OnShow();

            if (firstSelectedGameObject)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstSelectedGameObject);
            }
        }

        public void Hide()
        {
            OnHide();

            gameObject.SetActive(false);
        }

        public void Show(bool isShow)
        {
            if (isShow)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        protected virtual void OnShow() => Input.InputByDevice.Instance.SetInputMode(Input.InputMode.UI);
        protected virtual void OnHide() => Input.InputByDevice.Instance.SetInputMode(Input.InputMode.Player);
    }
}