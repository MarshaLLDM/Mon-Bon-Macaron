using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // �������� ���� using ��� TextMeshPro

public class GamePlayingTimerUI : MonoBehaviour
{
    // ������������ �������

    [SerializeField] private Image _timerImage;
    [SerializeField] private TextMeshProUGUI _timerText; // ����� ���� ��� ���������� ��������

    private void Update()
    {
        _timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();

        // ���������� ���������� ��������
        float remainingTime = GameManager.Instance.GetGamePlayingTimer();
        int remainingSeconds = Mathf.CeilToInt(remainingTime);
        _timerText.text = remainingSeconds.ToString() + " ���";
    }
}
