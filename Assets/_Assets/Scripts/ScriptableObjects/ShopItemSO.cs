using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public string itemDescription;
    public int itemPrice;
    public string itemPriceString;
}
