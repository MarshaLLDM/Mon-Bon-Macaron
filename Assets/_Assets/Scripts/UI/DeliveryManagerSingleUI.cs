using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    //Визуализация появления текста и иконок рецептов

    [SerializeField] private TextMeshProUGUI _recipeNameText;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Transform _iconTemplate;
    [SerializeField] private Image _progressBar; // Новый компонент для отображения прогресса времени

    private float _maxTime = 20f; // Максимальное время для рецепта
    private float _currentTimer;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO _recipeSO,float timer)
    {
        _recipeNameText.text = _recipeSO.recipeName;
        _currentTimer = timer;

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
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaing()) // Проверка состояния игры
        {
            _currentTimer -= Time.deltaTime;
            float progress = Mathf.Clamp01(_currentTimer / _maxTime);
            _progressBar.fillAmount = progress;
            _progressBar.color = Color.Lerp(Color.red, Color.green, progress);

            if (_currentTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}