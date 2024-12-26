using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    //Визуадизация иконки правильности доставки

    private const string POPUP = "Popup";

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _faliedColor;
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failedSprite;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);
        _backgroundImage.color = _faliedColor;
        _iconImage.sprite = _failedSprite;
        _messageText.text = "Неверный\nзаказ";
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);
        _backgroundImage.color = _successColor;
        _iconImage.sprite = _successSprite;
        _messageText.text = "Успешный\nзаказ";
    }
}
