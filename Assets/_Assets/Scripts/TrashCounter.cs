using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    // ����������� ������

    public static event EventHandler OnAnyObjectTrash; //������� ��� ��������������� �����

    new public static void ResetStartStatic()
    {
        OnAnyObjectTrash = null; // ����� ��������
    }

    public override void Interact(Player _player)
    {
        if (_player.HasKitchenObject())
        {
            _player.GetKitchenObject().DestroyObject();

            OnAnyObjectTrash?.Invoke(this, EventArgs.Empty);
        }
    }
}
