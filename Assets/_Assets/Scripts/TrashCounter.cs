using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    // Уничтожение мусора

    public static event EventHandler OnAnyObjectTrash; //Функция для воспроизведения звука

    new public static void ResetStartStatic()
    {
        OnAnyObjectTrash = null; // Сброс подписки
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
