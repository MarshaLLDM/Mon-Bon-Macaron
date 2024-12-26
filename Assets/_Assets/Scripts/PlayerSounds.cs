using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    //Звуки игрока

    private Player _player;
    private float _stepTimer;
    private float _stepTimerMax = .1f;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _stepTimer -= Time.deltaTime;
        if (_stepTimer < 0f )
        {
            _stepTimer = _stepTimerMax;

            if (_player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayStepSound(_player.transform.position, volume);
            }
        }
    }
}
