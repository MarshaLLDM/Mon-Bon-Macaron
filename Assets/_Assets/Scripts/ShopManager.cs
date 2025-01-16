using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private RecipeTracker _recipeTracker; // ������ �� RecipeTracker
    [SerializeField] private PurchaseManager _purchaseManager; // ������ �� PurchaseManager


    [Header("������ ����")]
    [SerializeField] private ShopItemSO[] _shopItems;
    [SerializeField] private GameObject _shopPanel; // ������ ��� ����������� ��������
    [SerializeField] private Transform _horizontalGroup; // ������ ��� ���������� ��������� ��������
    [SerializeField] private GameObject _itemPrefab; // ������ ��� ������� �������� ��������

    [Header("������ ����")]
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

            // ����� Image, ������� �������� �������� ���������, �� �� ������ � ����� �������
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

            var textComponents = itemContainer.GetComponentsInChildren<TextMeshProUGUI>(); // �������� ��������� Text ��� ����� ������
            if (textComponents.Length >= 2)
            {
                var primaryText = textComponents[0]; // �������� �����
                var secondaryText = textComponents[1]; // ������ �����

                // ������������� ����������� � ������
                if (childImage != null)
                {
                    childImage.sprite = shopItem.itemImage; // ������������� ����������� �� shopItem
                }

                if (primaryText != null)
                {
                    primaryText.text = shopItem.itemPriceString; // ������������� ��� ������
                }

                if (secondaryText != null)
                {
                    secondaryText.text = shopItem.itemName; // ������������� �������� ������
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

    // ������� ��� ����������� ���� �� �������
    private void ShowExtraWindow1(ShopItemSO shopItem)
    {
        _extraWindow1.SetActive(true);
        _extraText.text = shopItem.itemDescription; // ������������� ��� ������;
    }

    // ������� ��� ����������� ���� � ��������������� �����
    private void ShowExtraWindow2()
    {
        _extraWindow2.SetActive(true);
    }
}
