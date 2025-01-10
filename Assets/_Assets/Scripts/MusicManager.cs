using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //Воспроизвезедение музыки

    private const string PLAYER_PREFS_MUSIC_EFFECTS_VOLUME = "MusicVolume"; //Переменная для сохранения музыки

    public static MusicManager Instance { get; private set; }

    private AudioSource _audioSource;
    private float _volume = .3f;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_EFFECTS_VOLUME, .3f); //Загрузка информации значений о музыке
        _audioSource.volume = _volume;
    }
    public void SetVolume(float volume) //Функция изменение грмкости
    {
        _volume = volume;
        _audioSource.volume = _volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_EFFECTS_VOLUME, _volume); //Получение значения музыки
        PlayerPrefs.Save(); //Сохранение значения музыки
    }

    public float GetVolume()
    {
        return _volume;
    }
}
