using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    // Визуализация включения плиты и частиц

    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _particlesGameObject;

    private void Start()
    {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool _showVisual = e._state == StoveCounter.State.Frying || e._state == StoveCounter.State.Fried;
        _stoveOnGameObject.SetActive(_showVisual);
        _particlesGameObject.SetActive(_showVisual);
    }
}
