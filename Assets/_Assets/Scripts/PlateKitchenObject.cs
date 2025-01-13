using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : LinkKitchenObject
{
    //Скрипт для добавления ингридиентов на тарелку


    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObject _kitchenObject;
    }

    [Header("Допустимые ингридиенты")]
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList1; // Первый список допустимых объектов
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList2; // Второй список допустимых объектов
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList3; // Третий список допустимых объектов
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList4; // Третий список допустимых объектов
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList5; // Третий список допустимых объектов

    private List<KitchenObject> _kitchenObjectsList;
    private List<KitchenObject> _currentValidList; // Текущий активный список допустимых объектов

    private void Awake()
    {
        _kitchenObjectsList = new List<KitchenObject>();
        _currentValidList = null; // Изначально текущий список не установлен
    }

    public bool TryAddIngredient(KitchenObject _kitchenObject)
    {
        if (_currentValidList == null)
        {
            // Если текущий список не установлен, проверяем все списки
            if (IsValidIngredient(_kitchenObject, _validKitchenObjectsList1))
            {
                _currentValidList = _validKitchenObjectsList1;
            }
            else if (IsValidIngredient(_kitchenObject, _validKitchenObjectsList2))
            {
                _currentValidList = _validKitchenObjectsList2;
            }
            else if (IsValidIngredient(_kitchenObject, _validKitchenObjectsList3))
            {
                _currentValidList = _validKitchenObjectsList3;
            }
            else if (IsValidIngredient(_kitchenObject, _validKitchenObjectsList4))
            {
                _currentValidList = _validKitchenObjectsList4;
            }
            else if (IsValidIngredient(_kitchenObject, _validKitchenObjectsList5))
            {
                _currentValidList = _validKitchenObjectsList5;
            }
            else
            {
                return false; // Ингредиент не найден ни в одном из списков
            }
        }

        // Проверка допустимости ингредиента в текущем списке
        if (!IsValidIngredient(_kitchenObject, _currentValidList))
        {
            return false; // Объект не допустим для добавления
        }

        if (_kitchenObjectsList.Contains(_kitchenObject))
        {
            // Проверка на дублирование
            return false;
        }

        _kitchenObjectsList.Add(_kitchenObject);

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            _kitchenObject = _kitchenObject
        });
        return true;
    }

    private bool IsValidIngredient(KitchenObject _kitchenObject, List<KitchenObject> validList)
    {
        return validList.Contains(_kitchenObject);
    }

    public List<KitchenObject> GetKitchenObjectsList()
    {
        return _kitchenObjectsList;
    }
}
