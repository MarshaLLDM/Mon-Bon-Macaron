using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Добавьте этот using для TextMeshPro

public class GamePlayingTimerUI : MonoBehaviour
{
    // Визуализация таймера

    [SerializeField] private Image _timerImage;
    [SerializeField] private TextMeshProUGUI _timerText; // Новое поле для текстового элемента

    private void Update()
    {
        _timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();

        // Обновление текстового элемента
        float remainingTime = GameManager.Instance.GetGamePlayingTimer();
        int remainingSeconds = Mathf.CeilToInt(remainingTime);
        _timerText.text = remainingSeconds.ToString() + " сек";
    }
}
