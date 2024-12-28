using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere; //Функция для воспроизведения звука

    public static void ResetStartStatic()
    {
        OnAnyObjectPlacedHere = null; // Сброс подписки
    }

    [SerializeField] private Transform _counterTopPoint;

<<<<<<< HEAD
=======

>>>>>>> parent of 0b1e70b (test)

    private LinkKitchenObject _onkitchenObjects; // что лежит на контейнере

    public virtual void Interact(Player _player)
    {
        Debug.LogError("Срабатывет базовый счетчик");
    }

    public virtual void InteractAlternate(Player _player)
    {
       // Debug.LogError("Срабатывет альтернативный базовый счетчик");
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
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty); //Вызов функции для воспроизведения звука
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
