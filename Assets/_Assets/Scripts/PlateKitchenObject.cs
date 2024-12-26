using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : LinkKitchenObject
{
    // Start is called before the first frame update

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs: EventArgs
    {
        public KitchenObject _kitchenObject;
    }


    [SerializeField] private List<KitchenObject> _validKitchenObjectsList; //������ ���������� ��������, ������� ����� �������� �� �������

    private List<KitchenObject> _kitchenObjectsList;

    private void Awake()
    {
        _kitchenObjectsList = new List<KitchenObject>();
    }

    public bool TryAddIngredient(KitchenObject _kitchenObject)
    {
        if (!_validKitchenObjectsList.Contains(_kitchenObject)) //���� � ������ ���� ���������� ������ � �� ���������, ������� �� ����� ��������, �� ���������� ����������
        {
            return false; //������� � ������ ���
        }
        if (_kitchenObjectsList.Contains(_kitchenObject))
        {
            //�������� ����-�� � ������ ��� �������� ��������
            return false;
        }
        else
        {
            _kitchenObjectsList.Add(_kitchenObject);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                _kitchenObject = _kitchenObject
            });
            return true;
        }
    }
    public List<KitchenObject> GetKitchenObjectsList()
    {
        return _kitchenObjectsList;
    }
    
}
