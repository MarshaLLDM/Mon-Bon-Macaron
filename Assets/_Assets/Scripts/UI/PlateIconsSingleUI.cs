using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Image _image;
   public void SetKitchenObjectSO(KitchenObject _kitchenObject)
    {
        _image.sprite = _kitchenObject._sprite;
    }
}
