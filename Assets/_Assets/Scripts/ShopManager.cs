using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private RecipeTracker _recipeTracker; // Ссылка на RecipeTracker
    [SerializeField] private PurchaseManager _purchaseManager; // Ссылка на PurchaseManager


    [Header("Первое окно")]
    [SerializeField] private ShopItemSO[] _shopItems;
    [SerializeField] private GameObject _shopPanel; // Панель для отображения магазина
    [SerializeField] private Transform _horizontalGroup; // Объект для размещения элементов магазина
    [SerializeField] private GameObject _itemPrefab; // Префаб для каждого элемента магазина

    [Header("Второе окно")]
    [SerializeField] private GameObject _extraWindow1;
    [SerializeField] private GameObject _extraWindow2;
    [SerializeField] private TextMeshProUGUI _extraText;



    private void Start()
    {
        PopulateShopPanel();
        _shopPanel.SetActive(false);
        _extraWindow1.SetActive(false);
        _extraWindow2.SetActive(false);
    }

    private void PopulateShopPanel()
    {
        foreach (var shopItem in _shopItems)
        {
            var itemContainer = Instantiate(_itemPrefab, _horizontalGroup.transform);
            var button = itemContainer.GetComponentInChildren<Button>();

            // Поиск Image, который является дочерним элементом, но не связан с самой кнопкой
            Image childImage = null;
            foreach (Transform child in button.transform)
            {
                var img = child.GetComponent<Image>();
                if (img != null)
                {
                    childImage = img;
                    break;
                }
            }

            var textComponents = itemContainer.GetComponentsInChildren<TextMeshProUGUI>(); // Получаем компонент Text для имени товара
            if (textComponents.Length >= 2)
            {
                var primaryText = textComponents[0]; // Основной текст
                var secondaryText = textComponents[1]; // Второй текст

                // Устанавливаем изображения и тексты
                if (childImage != null)
                {
                    childImage.sprite = shopItem.itemImage; // Устанавливаем изображение из shopItem
                }

                if (primaryText != null)
                {
                    primaryText.text = shopItem.itemPriceString; // Устанавливаем имя товара
                }

                if (secondaryText != null)
                {
                    secondaryText.text = shopItem.itemName; // Устанавливаем описание товара
                }
            }
            button.onClick.AddListener(() => OnItemButtonClicked(shopItem));
        }
    }

    public void ShowShopPanel()
    {
        _shopPanel.SetActive(true);
    }

    private void OnItemButtonClicked(ShopItemSO _itemPrice)
    {
        if (_purchaseManager.TryPurchaseItem(_itemPrice))
        {
            ShowExtraWindow1(_itemPrice);
        }
        else
        {
            ShowExtraWindow2();
        }
    }

    // Функция для отображения окна со скидкой
    private void ShowExtraWindow1(ShopItemSO shopItem)
    {
        _extraWindow1.SetActive(true);
        _extraText.text = shopItem.itemDescription; // Устанавливаем имя товара;
    }

    // Функция для отображения окна о недостаточности монет
    private void ShowExtraWindow2()
    {
        _extraWindow2.SetActive(true);
    }
}
