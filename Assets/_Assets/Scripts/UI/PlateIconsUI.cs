using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    // Визуализация какие ингредиенты уже положены на тарелку

    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += _kitchenObject_OnIngredientAdded;
    }

    private void _kitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObject _kitchenObject in _plateKitchenObject.GetKitchenObjectsList())
        {
            Transform _iconTransform = Instantiate(_iconTemplate, transform);
            _iconTransform.gameObject.SetActive(true);
            _iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(_kitchenObject);
        }
    }
}
