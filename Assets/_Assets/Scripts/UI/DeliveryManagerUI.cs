using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    //Визуализация добавления шаблонов

    [SerializeField] private Transform _container;
    [SerializeField] private Transform _recipeTemplate;

    private void Awake()
    {
        _recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeleveryManager_OnRecipeCompleted;

        UpdateVisual();
    }

    private void DeleveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeWithTimer _recipeSO in DeliveryManager.Instance.GetWaitingRecipeList())
        {
            Transform _recipeTransform = Instantiate(_recipeTemplate, _container);
            _recipeTransform.gameObject.SetActive(true);
            _recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(_recipeSO.Recipe, _recipeSO.Timer);
        }
    }
}
