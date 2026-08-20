using UnityEngine;

public class UIScreen : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);

        OnShow();
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

    protected virtual void OnShow() { }
    protected virtual void OnHide() { }
}