using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    //Скрипт для обработки логики покупок
    private const string PurchasedItemsKey = "PurchasedItems";
    private HashSet<string> purchasedItems = new HashSet<string>();

    [SerializeField] private RecipeTracker recipeTracker;

    private void Awake()
    {
        LoadPurchasedItems();
    }

    public bool TryPurchaseItem(ShopItemSO item)
    {
        if (IsItemPurchased(item))
        {
            return true;
        }

        int currentCoins = recipeTracker.GetSuccessedRecipedAmount();
        if (currentCoins >= item.itemPrice)
        {
            recipeTracker.DecrementSuccessedRecipedAmount(item.itemPrice);
            purchasedItems.Add(item.itemName);
            SavePurchasedItems();
            return true;
        }

        return false;
    }

    public bool IsItemPurchased(ShopItemSO item)
    {
        return purchasedItems.Contains(item.itemName);
    }

    private void SavePurchasedItems()
    {
        PlayerPrefs.SetString(PurchasedItemsKey, string.Join(",", purchasedItems));
        PlayerPrefs.Save();
    }

    private void LoadPurchasedItems()
    {
        string purchasedItemsString = PlayerPrefs.GetString(PurchasedItemsKey, string.Empty);
        if (!string.IsNullOrEmpty(purchasedItemsString))
        {
            purchasedItems = new HashSet<string>(purchasedItemsString.Split(','));
        }
    }
}
