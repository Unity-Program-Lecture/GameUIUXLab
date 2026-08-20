using UnityEngine;
using TMPro;
using KidzDev.Unity.SlicedFillImage;

public class HUDView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private SlicedFilledImage healthImageSliced;

    private int _score;
    private float _remainTime = 60f;
    private float _health = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _score = 0;
        _remainTime = 60f;
        healthImageSliced.fillAmount = 1f;
        Refresh();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void AddScoreAndDamage()
    {
        _score += 10;
        _health = Mathf.Max(0f, _health - 10f);

        Refresh();
    }

    private void Refresh()
    {
        scoreText.text = _score.ToString();
        timeText.text = _remainTime.ToString("F1");
        healthImageSliced.fillAmount = _health / 100f;
    }
}
