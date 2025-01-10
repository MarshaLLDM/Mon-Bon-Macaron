using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    //������������ ���������� ��������

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Slider _soundEffectsSlider; // �������� ��������� ��������� ������
    [SerializeField] private Slider _musicEffectsSlider; // �������� ��������� ��������� ������
    [SerializeField] private Button _closeEffects; //������ ��������
    [SerializeField] private TextMeshProUGUI _soundEffectsText;
    [SerializeField] private TextMeshProUGUI _musicEffectsText;

    private Action _onCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        _soundEffectsSlider.onValueChanged.AddListener((value) =>
        {
            SoundManager.Instance.SetVolume(value);
            UpdateVisual();
        });

        _musicEffectsSlider.onValueChanged.AddListener((value) =>
        {
            MusicManager.Instance.SetVolume(value);
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
        _soundEffectsSlider.value = SoundManager.Instance.GetVolume();
        _musicEffectsSlider.value = MusicManager.Instance.GetVolume();
        _soundEffectsText.text = "����: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        _musicEffectsText.text = "������: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
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
