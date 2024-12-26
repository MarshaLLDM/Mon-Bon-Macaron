using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainersCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject;// взаимодействие игрока с объектом

    [SerializeField] private KitchenObject _kitchenObject;

    public override void Interact(Player _player) //игрок взаимодействуй и предмет отдается сразу игроку
    {
        if (!_player.HasKitchenObject())
        {
            //У игрока в руках ничего нет
            LinkKitchenObject.SpawnKitchenObject(_kitchenObject, _player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}