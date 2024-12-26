using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class BlenderCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; //Обработка визуального прогресса
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State _state;
    }
    public enum State
    {
        Idle, //Состояние ожидания плиты
        Frying, //Объект жарится на плите
    }

    // Готовка плиты

    [SerializeField] private BlenderRecipeSO[] _blenderRecipeSOArray;


    private State _state;

    private float _fryingTimer; //Таймер жарки мяса
    private BlenderRecipeSO _blenderRecipeSO;

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;


                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        _progressNormalized = _fryingTimer / _blenderRecipeSO._blenderTimerMax
                    });


                    if (_fryingTimer > _blenderRecipeSO._blenderTimerMax)
                    {
                        //Если значение больше, то блюдо прожарилось
                        GetKitchenObject().DestroyObject(); //Берем объект и уничтожаем его

                        LinkKitchenObject.SpawnKitchenObject(_blenderRecipeSO.output, this); //Создание нового объекта

                        _state = State.Idle; //Переключение состояния плиты, где объект приготовился, но продолжает жариться
                    }
                break;
            /*    case State.Burned:
                    break;*/
            }
            //  Debug.Log(_state);
        }
    }

    public override void Interact(Player _player)
    {
        if (!HasKitchenObject())
        {
            //Значит ничего нет
            if (_player.HasKitchenObject())
            {
                //  Персонаж что-то держит в руках
                if (HasRecipeWithInput(_player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Показывает то что можно пожарить
                    _player.GetKitchenObject().SetKitchenObjectParent(this);

                    _blenderRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.Frying; //Переключение состояния плиты на прожарку
                    _fryingTimer = 0f; //Сброс таймера

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        _state = _state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        _progressNormalized = _fryingTimer / _blenderRecipeSO._blenderTimerMax
                    });
                }
            }
            else
            {
                //У игрока ничего нет
            }
        }
        else
        {
            //Есть кухонный объект
            if (_player.HasKitchenObject())
            {
                //У игрока что-то есть
                if (_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject _plateKitchenObject)) //Если есть тарелка в руках
                {
                    if (_plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroyObject();

                        _state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            _state = _state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            _progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //У игрока ничего нет и мы подбираем то что лежит на объекте
                GetKitchenObject().SetKitchenObjectParent(_player);

                _state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    _state = _state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    _progressNormalized = 0f
                });
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObject _inputKitchenObjectSO)
    {
        BlenderRecipeSO _blenderRecipeSO = GetFryingRecipeSOWithInput(_inputKitchenObjectSO);
        return _blenderRecipeSO != null;
    }

    private KitchenObject GetOutputForInput(KitchenObject _inputKitchenObjectSO)
    {
        BlenderRecipeSO _blenderRecipeSO = GetFryingRecipeSOWithInput(_inputKitchenObjectSO);
        if (_blenderRecipeSO != null)
        {
            return _blenderRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private BlenderRecipeSO GetFryingRecipeSOWithInput(KitchenObject _inputKitchenObjectSO)
    {
        foreach (BlenderRecipeSO _blenerRecipeSO in _blenderRecipeSOArray)
        {
            if (_blenerRecipeSO.input == _inputKitchenObjectSO)
            {
                return _blenerRecipeSO;
            }
        }
        return null;
    }
}
