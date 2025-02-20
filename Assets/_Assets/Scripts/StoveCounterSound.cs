using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    //���� �����

    [SerializeField] private StoveCounter _stoveCounter;

    private AudioSource _audioSource;

    private float _warningSOundTimer;

    private bool _playWarningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool _playSound = e._state == StoveCounter.State.Frying;
        if (_playSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void Update()
    {
        if (_playWarningSound)
        {
            _warningSOundTimer -= Time.deltaTime;
            if (_warningSOundTimer <= 0f)
            {
                float _warningSOundTimerMax = .2f;
                _warningSOundTimer = _warningSOundTimerMax;

                SoundManager.Instance.PlayWarningSOund(_stoveCounter.transform.position);
            }
        }
    }
}
