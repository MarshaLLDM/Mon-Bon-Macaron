using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    // Появление тарелок

    public event EventHandler OnPlateSpawned; //Событие появления тарелки
    public event EventHandler OnPlateRemoved; //Событие удаления тарелки

    [SerializeField] private KitchenObject _plateKitchenObject;

    private float _spawnPlateTimer;
    private float _spawnPlateTimerMax = 4f; //Переменная через сколько появится тарелка
    private int _platesSpawnedAmount;
    private int _platesSpawnedAmountMax = 4; //Можно создать максимально 4 тарелки

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if ( _spawnPlateTimer > _spawnPlateTimerMax)
        {
            _spawnPlateTimer = 0f;

            if (_platesSpawnedAmount < _platesSpawnedAmountMax)
            {
                _platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player _player)
    {
        if(!_player.HasKitchenObject())
        {
            //У игрока ничего нет
            if(_platesSpawnedAmount > 0)
            {
                //Есть хотя-бы одна тарелка и мы будем отдавать её игроку
                _platesSpawnedAmount--;

                LinkKitchenObject.SpawnKitchenObject(_plateKitchenObject, _player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
