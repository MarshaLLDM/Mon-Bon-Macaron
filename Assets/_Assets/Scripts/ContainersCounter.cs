using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainersCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject;// �������������� ������ � ��������

    [SerializeField] private KitchenObject _kitchenObject;

    public override void Interact(Player _player) //����� �������������� � ������� �������� ����� ������
    {
        if (!_player.HasKitchenObject())
        {
            //� ������ � ����� ������ ���
            LinkKitchenObject.SpawnKitchenObject(_kitchenObject, _player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}