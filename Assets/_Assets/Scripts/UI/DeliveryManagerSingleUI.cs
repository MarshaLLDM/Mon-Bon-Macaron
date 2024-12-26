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


    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO _recipeSO)
    {
        _recipeNameText.text = _recipeSO.recipeName;


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
}