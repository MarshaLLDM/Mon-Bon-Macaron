using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    //ќтображение визуализации паузы

    [SerializeField] private Button _resumeButton; // нопка возобновлени€ игры
    [SerializeField] private Button _mainMenuButton; // нопка выхода в главное меню
    [SerializeField] private Button _optionsMenuButton; // нопка настроек в главное меню

    private void Awake()
    {
        _resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResumeGame();
        });

        _mainMenuButton.onClick.AddListener(() =>
        {
            Loading.Load(Loading.Scene.MainMenu);
        });

        _optionsMenuButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }
    private void Start()
    {
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnPause += GameManager_OnGameUnPause;

        Hide();
    }

    private void GameManager_OnGameUnPause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePause(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
