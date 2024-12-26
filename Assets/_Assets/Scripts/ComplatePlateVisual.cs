using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplatePlateVisual : MonoBehaviour
{
    // Start is called before the first frame update

    [Serializable]
    public struct KitchenObject_GameObject
    {
        public KitchenObject _kitchenObject;
        public GameObject _gameObject;
    }


    [SerializeField] private PlateKitchenObject _kitchenObject;
    [SerializeField] private List<KitchenObject_GameObject> _kitchenObjectGameObjectsList;

    private void Start()
    {
        _kitchenObject.OnIngredientAdded += _kitchenObject_OnIngredientAdded;

        foreach (KitchenObject_GameObject _kitchenObjectGameObject in _kitchenObjectGameObjectsList)
        {
                _kitchenObjectGameObject._gameObject.SetActive(false);
        }
    }

    private void _kitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(KitchenObject_GameObject _kitchenObjectGameObject in _kitchenObjectGameObjectsList)
        {
            if(_kitchenObjectGameObject._kitchenObject == e._kitchenObject) //Если есть совпадение объектов, то будет true
            {
                _kitchenObjectGameObject._gameObject.SetActive(true);
            }
        }
    }
}
