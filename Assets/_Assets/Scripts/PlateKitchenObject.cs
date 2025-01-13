using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : LinkKitchenObject
{
    //������ ��� ���������� ������������ �� �������


    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObject _kitchenObject;
    }

    [Header("���������� �����������")]
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList1; // ������ ������ ���������� ��������
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList2; // ������ ������ ���������� ��������
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList3; // ������ ������ ���������� ��������
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList4; // ������ ������ ���������� ��������
    [SerializeField] private List<KitchenObject> _validKitchenObjectsList5; // ������ ������ ���������� ��������

    private List<KitchenObject> _kitchenObjectsList;
    private List<KitchenObject> _currentValidList; // ������� �������� ������ ���������� ��������

    private void Awake()
    {
        _kitchenObjectsList = new List<KitchenObject>();
        _currentValidList = null; // ���������� ������� ������ �� ����������
    }

    public bool TryAddIngredient(KitchenObject _kitchenObject)
    {
        if (_currentValidList == null)
        {
            // ���� ������� ������ �� ����������, ��������� ��� ������
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
                return false; // ���������� �� ������ �� � ����� �� �������
            }
        }

        // �������� ������������ ����������� � ������� ������
        if (!IsValidIngredient(_kitchenObject, _currentValidList))
        {
            return false; // ������ �� �������� ��� ����������
        }

        if (_kitchenObjectsList.Contains(_kitchenObject))
        {
            // �������� �� ������������
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
