using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnGameUI : MonoBehaviour
{
    //Визуализация интерфейса паузы

    [SerializeField] private Button _pauseButton; //Кнопка паузы игры

    private void Awake()
    {
        _pauseButton.onClick.AddListener(() =>
        {
            GameManager.Instance.PauseGame();
        });

    }
}
