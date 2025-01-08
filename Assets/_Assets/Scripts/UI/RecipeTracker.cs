using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipeTracker : MonoBehaviour
{
    private const string SuccessedRecipedAmountKey = "SuccessedRecipedAmount";
    private int _successedRecipedAmount;

    [SerializeField] private TextMeshProUGUI successedRecipedText;

    private void Awake()
    {
        LoadSuccessedRecipedAmount();
        UpdateSuccessedRecipedText();
        if (DeliveryManager.Instance != null)
        {
            DeliveryManager.Instance.OnRecipeSuccess += HandleRecipeSuccess; // Подписка на событие
        }
    }

    private void OnDestroy()
    {
        if (DeliveryManager.Instance != null)
        {
            DeliveryManager.Instance.OnRecipeSuccess -= HandleRecipeSuccess; // Отписка от события
        }
    }

    private void HandleRecipeSuccess(object sender, System.EventArgs e)
    {
        successedRecipedText.text = DeliveryManager.Instance.GetSucceseedRecipedMax().ToString();
        SaveSuccessedRecipedAmount();
        UpdateSuccessedRecipedText();
    }

    public void IncrementSuccessedRecipedAmount()
    {
        _successedRecipedAmount++;
        SaveSuccessedRecipedAmount();
        UpdateSuccessedRecipedText();
    }

    public int GetSuccessedRecipedAmount()
    {
        return _successedRecipedAmount;
    }

    private void SaveSuccessedRecipedAmount()
    {
        PlayerPrefs.SetInt(SuccessedRecipedAmountKey, _successedRecipedAmount);
        PlayerPrefs.Save();
    }

    private void LoadSuccessedRecipedAmount()
    {
        _successedRecipedAmount = PlayerPrefs.GetInt(SuccessedRecipedAmountKey, 0);
    }

    public void ResetSuccessedRecipedAmount()
    {
        _successedRecipedAmount = 0;
        SaveSuccessedRecipedAmount();
        UpdateSuccessedRecipedText();
    }

    private void UpdateSuccessedRecipedText()
    {
        if (successedRecipedText != null)
        {
            successedRecipedText.text = $"{_successedRecipedAmount}";
        }
    }

    public void DecrementSuccessedRecipedAmount(int amount)
    {
        _successedRecipedAmount -= amount;
        SaveSuccessedRecipedAmount();
        UpdateSuccessedRecipedText();
    }
}
