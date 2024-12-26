using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    //������

    [SerializeField] private Button _playButton; //������ ����
    [SerializeField] private Button _settingsButton; //������ ��������

    private void Awake()
    {
        _playButton.onClick.AddListener(() => //������ ���������
        {
            Loading.Load(Loading.Scene.Game); //�������� ����� ����
        }); 

        _settingsButton.onClick.AddListener(() => //������ ��������� 
        {

        });

        Time.timeScale = 1f;
    }

    private void PlayClick()
    {

    }
}
