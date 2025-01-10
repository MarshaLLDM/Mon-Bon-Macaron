using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    //Кнопки

    [SerializeField] private Button _playButton; //Кнопка игры

    private void Awake()
    {
        _playButton.onClick.AddListener(() => //Лямбда выражения
        {
            Loading.Load(Loading.Scene.Game); //Загрузка сцены игры
        }); 

        Time.timeScale = 1f;
    }

    private void PlayClick()
    {

    }
}
