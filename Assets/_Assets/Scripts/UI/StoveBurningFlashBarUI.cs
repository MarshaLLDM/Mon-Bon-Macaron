using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBurningFlashBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private Image _progressBarImage; // Добавляем ссылку на Image компонента прогресса

    private void Start()
    {
        _stoveCounter.OnProgressChanged += stoveCounter_OnProgressChanged;
    }

    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        // Плавное изменение цвета прогресса в зависимости от значения прогресса
        if (e._progressNormalized < 0.5f)
        {
            // От зеленого к желтому
            _progressBarImage.color = Color.Lerp(Color.red, Color.yellow, e._progressNormalized * 2);
        }
        else
        {
            // От желтого к красному
            _progressBarImage.color = Color.Lerp(Color.yellow, Color.green, (e._progressNormalized - 0.5f) * 2);
        }
    }
}
