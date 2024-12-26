using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    //Визуализация интерфейса настроек

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button _soundEffects; //Кнопка изменения громкрости звуков
    [SerializeField] private Button _musicEffects; //Кнопка изменения громкрости музыки
    [SerializeField] private Button _closeEffects; //Кнопка закрытия
    [SerializeField] private TextMeshProUGUI _soundEffectsTExt;
    [SerializeField] private TextMeshProUGUI _musicEffectsTExt;

    private Action _onCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        _soundEffects.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        _musicEffects.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        _closeEffects.onClick.AddListener(() =>
        {
            Hide();
            _onCloseButtonAction();
        });
    }

    private void Start()
    {
        UpdateVisual();
        Hide();
    }

    private void UpdateVisual()
    {
        _soundEffectsTExt.text = "Звук: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        _musicEffectsTExt.text = "Музыка: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
    }

    public void Show(Action _onCloseButtonAction)
    {
        this._onCloseButtonAction = _onCloseButtonAction;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
