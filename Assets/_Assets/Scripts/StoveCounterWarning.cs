using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterWarning : MonoBehaviour
{
    //Визуалицаия иконки предупреждения о пережарки

    [SerializeField] private StoveCounter _stoveCounter;

    private void Start()
    {
        _stoveCounter.OnProgressChanged += stoveCounter_OnProgressChanged;

        Hide();
    }

    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool _show = _stoveCounter.IsFried() && e._progressNormalized >= burnShowProgressAmount; //Когда будет нужное состояние печи и прогресс больше 0,5

        if (_show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
