using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurningFlashBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private Image _progressBarImage; // ��������� ������ �� Image ���������� ���������

    private void Start()
    {
        _stoveCounter.OnProgressChanged += stoveCounter_OnProgressChanged;
    }

    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        // ������� ��������� ����� ��������� � ����������� �� �������� ���������
        if (e._progressNormalized < 0.5f)
        {
            // �� �������� � �������
            _progressBarImage.color = Color.Lerp(Color.red, Color.yellow, e._progressNormalized * 2);
        }
        else
        {
            // �� ������� � ��������
            _progressBarImage.color = Color.Lerp(Color.yellow, Color.green, (e._progressNormalized - 0.5f) * 2);
        }
    }
}
