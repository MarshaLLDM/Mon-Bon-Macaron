using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere; //������� ��� ��������������� �����

    public static void ResetStartStatic()
    {
        OnAnyObjectPlacedHere = null; // ����� ��������
    }

    [SerializeField] private Transform _counterTopPoint;

<<<<<<< HEAD
=======

>>>>>>> parent of 0b1e70b (test)

    private LinkKitchenObject _onkitchenObjects; // ��� ����� �� ����������

    public virtual void Interact(Player _player)
    {
        Debug.LogError("���������� ������� �������");
    }

    public virtual void InteractAlternate(Player _player)
    {
       // Debug.LogError("���������� �������������� ������� �������");
    }
    public Transform GetKitchenOjectFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKitchenObject(LinkKitchenObject _onkitchenObjects)
    {
        this._onkitchenObjects = _onkitchenObjects;

        if(_onkitchenObjects != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty); //����� ������� ��� ��������������� �����
        }
    }
    public LinkKitchenObject GetKitchenObject()
    {
        return _onkitchenObjects;
    }

    public void ClearKitchenObject()
    {
        _onkitchenObjects = null;
    }

    public bool HasKitchenObject()
    {
        return _onkitchenObjects != null;
    }
}
