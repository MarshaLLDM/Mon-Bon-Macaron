using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountUI : MonoBehaviour
{
    //Показ и скрытие начало отсчета

    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI _countStartText;

    private Animator _animator;

    private int _previousCountDownNumber;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStatedChanged += GameManager_OnStatedChanged;

        Hide();
    }

    private void GameManager_OnStatedChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountStartTimer());
        _countStartText.text = countDownNumber.ToString(); //Вывод счетчика и преобразования float в int

        if(_previousCountDownNumber != countDownNumber)
        {
            _previousCountDownNumber = countDownNumber;
            _animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountDownSound();
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
