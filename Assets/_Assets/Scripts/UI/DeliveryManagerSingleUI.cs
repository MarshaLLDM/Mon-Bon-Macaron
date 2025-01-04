using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    // Визуализация появления текста и иконок рецептов

    [SerializeField] private TextMeshProUGUI _recipeNameText; // Текстовое поле для отображения названия рецепта
    [SerializeField] private Transform _iconContainer; // Контейнер для иконок ингредиентов рецепта
    [SerializeField] private Transform _iconTemplate; // Шаблон иконки ингредиента


    [SerializeField] private Image _backgroundImage; // Изображение для фона, которое будет изменять цвет
    private RecipeSO _recipeSO; // Текущий рецепт
    private int _recipeIndex; // Индекс текущего рецепта
    private float _maxTime; // Максимальное время для выполнения рецепта

    [SerializeField] private Color _startColor; // Зеленый цвет для фона длительнисти рецепта
    [SerializeField] private Color _endColor; // Красный цвет для фона длительнисти рецепта

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO, int recipeIndex, float maxTime)
    {
        _recipeSO = recipeSO;
        _recipeIndex = recipeIndex;
        _maxTime = maxTime;

        _recipeNameText.text = _recipeSO.recipeName;

        foreach (Transform child in _iconContainer)
        {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObject _kitchenObject in _recipeSO._kitchenObjects)
        {
            Transform _iconTransform = Instantiate(_iconTemplate, _iconContainer);
            _iconTransform.gameObject.SetActive(true);
            _iconTransform.GetComponent<Image>().sprite = _kitchenObject._sprite;
        }

        if (!GameManager.Instance.IsGamePlaing())
        {
            return;
        }

        float remainingTime = DeliveryManager.Instance.GetRemainingTimeForRecipe(_recipeIndex);
        float fillAmount = Mathf.Clamp01(remainingTime / _maxTime);
        _backgroundImage.fillAmount = fillAmount;
        _backgroundImage.color = Color.Lerp(_endColor, _startColor, fillAmount);

        if (remainingTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {

    }
}
