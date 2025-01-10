using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //����������������� ������

    private const string PLAYER_PREFS_MUSIC_EFFECTS_VOLUME = "MusicVolume"; //���������� ��� ���������� ������

    public static MusicManager Instance { get; private set; }

    private AudioSource _audioSource;
    private float _volume = .3f;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_EFFECTS_VOLUME, .3f); //�������� ���������� �������� � ������
        _audioSource.volume = _volume;
    }
    public void SetVolume(float volume) //������� ��������� ��������
    {
        _volume = volume;
        _audioSource.volume = _volume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_EFFECTS_VOLUME, _volume); //��������� �������� ������
        PlayerPrefs.Save(); //���������� �������� ������
    }

    public float GetVolume()
    {
        return _volume;
    }
}
