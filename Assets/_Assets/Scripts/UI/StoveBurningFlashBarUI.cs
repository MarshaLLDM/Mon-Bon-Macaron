using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningFlashBarUI : MonoBehaviour
{
    //Визуализация меняющееся цвета индикатора прогресса бара

    private const string IS_FLASHING = "IsFlashing"; //Параметр для аниматора

    [SerializeField] private StoveCounter _stoveCounter;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stoveCounter.OnProgressChanged += stoveCounter_OnProgressChanged;

        _animator.SetBool(IS_FLASHING, false);
    }

    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool _show = _stoveCounter.IsFried() && e._progressNormalized >= burnShowProgressAmount; //Когда будет нужное состояние печи и прогресс больше 0,5

        _animator.SetBool(IS_FLASHING, _show);
    }
}
