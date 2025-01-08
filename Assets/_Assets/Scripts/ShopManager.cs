using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private Image _backGround;
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

            var text = itemContainer.GetComponentInChildren<TextMeshProUGUI>(); // �������� ��������� Text ��� ����� ������

            // ������������� ����������� � �����
            if (childImage != null)
            {
                childImage.sprite = shopItem.itemImage; // ������������� ����������� �� shopItem
            }

            if (text != null)
            {
                text.text = shopItem.itemName; // ������������� ��� ������
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

   /* private void DisableBackground() // ������� ��� ���������� ����
    {
        _backGround.gameObject.SetActive(false);
    }

    private void EnableBackground() // ������� ��� ��������� ����
    {
        _backGround.gameObject.SetActive(true);
    }*/
}
