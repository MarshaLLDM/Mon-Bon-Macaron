using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ObjectSelectionUI : MonoBehaviour
{

    //Визуализация появления окна с выбором ингридиентов


    [SerializeField] private GameObject _selectionPanel; //Объект для отображения окна
    [SerializeField] private GameObject _horizontalGroup; //Объект для спавна ингридиентов
    [SerializeField] private GameObject _containerPrefab; // Префаб контейнера с дочерней кнопкой
    [SerializeField] private List<KitchenObject> kitchenObjects; //Список ингридиентов

    private Player _playerMovement; // Ссылка на скрипт движения игрока

    public event Action<KitchenObject> OnObjectSelected;

    private void Start()
    {
        // Найти объект игрока по тегу
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _playerMovement = player.GetComponent<Player>();
        }

        PopulateSelectionPanel();
        _selectionPanel.SetActive(false);
    }

    private void PopulateSelectionPanel()
    {
        foreach (var kitchenObject in kitchenObjects)
        {
            var container = Instantiate(_containerPrefab, _horizontalGroup.transform);
            var button = container.GetComponentInChildren<Button>();
            var image = button.GetComponent<Image>(); // Получаем компонент Image на дочерней кнопке
            image.sprite = kitchenObject._sprite; // Устанавливаем изображение из kitchenObject
            button.onClick.AddListener(() => SelectObject(kitchenObject));
        }
    }

    public void ShowSelectionPanel()
    {
        _selectionPanel.SetActive(true);
        _playerMovement.enabled = false;
    }

    private void SelectObject(KitchenObject kitchenObject)
    {
        OnObjectSelected?.Invoke(kitchenObject);
        _selectionPanel.SetActive(false);
        _playerMovement.enabled = true;
    }
}
