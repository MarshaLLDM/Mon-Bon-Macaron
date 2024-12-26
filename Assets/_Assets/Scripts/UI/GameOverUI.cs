using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    //Визуализация проирыша

    [SerializeField] private TextMeshProUGUI _gameOverText;

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
