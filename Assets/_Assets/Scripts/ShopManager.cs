using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private Image _backGround;
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
      //  DisableBackground();
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

            var text = itemContainer.GetComponentInChildren<TextMeshProUGUI>(); // Получаем компонент Text для имени товара

            // Устанавливаем изображение и текст
            if (childImage != null)
            {
                childImage.sprite = shopItem.itemImage; // Устанавливаем изображение из shopItem
            }

            if (text != null)
            {
                text.text = shopItem.itemName; // Устанавливаем имя товара
            }

            button.onClick.AddListener(() => OnItemButtonClicked(shopItem));
        }
    }

    public void ShowShopPanel()
    {
        //EnableBackground();
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

   /* private void DisableBackground() // Функция для отключения фона
    {
        _backGround.gameObject.SetActive(false);
    }

    private void EnableBackground() // Функция для включения фона
    {
        _backGround.gameObject.SetActive(true);
    }*/
}
