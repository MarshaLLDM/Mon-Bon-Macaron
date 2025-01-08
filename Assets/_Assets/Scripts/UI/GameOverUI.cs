using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    //������������ ��������

    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _restartButton; // ������ ��������
    [SerializeField] private Button _mainMenuButton; //������ ������ � ������� ����

    private void Awake()
    {
        _restartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.RestartGame();
            Hide();
        });

        _mainMenuButton.onClick.AddListener(() =>
        {
            Loading.Load(Loading.Scene.MainMenu);
        });
    }
    private void Start()
    {
        GameManager.Instance.OnStatedChanged += GameManager_OnStatedChanged;

        Hide();
    }

    private void GameManager_OnStatedChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();

            _gameOverText.text = DeliveryManager.Instance.GetSucceseedReciped().ToString();
        }
        else
        {
            Hide();
        }
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
