using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
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
        Fried, //Объект готов, но продлолжает жариться
        Burned, //Сгоревший объект
    }

    // Готовка плиты

    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSOArray;


    private State _state;

    private float _fryingTimer; //Таймер жарки мяса
    private FryingRecipeSO _fryingRecipeSO;
    private float _burningTimer; //Таймер горения
    private BurningRecipeSO _burningRecipeSO;

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
                        _progressNormalized = _fryingTimer / _fryingRecipeSO._fryingTimerMax
                    });


                    if (_fryingTimer > _fryingRecipeSO._fryingTimerMax)
                    {
                        //Если значение больше, то блюдо прожарилось
                        GetKitchenObject().DestroyObject(); //Берем объект и уничтожаем его

                        LinkKitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this); //Создание нового объекта

                        _state = State.Burned; //Переключение состояния плиты, где объект приготовился, но продолжает жариться
                        _burningTimer = 0f;
                      //  _burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            _state = _state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            _progressNormalized = 0f
                        });
                    }
                    break;
              /*  case State.Fried:
                    _burningTimer += Time.deltaTime;


                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        _progressNormalized = _burningTimer / _burningRecipeSO._burningTimeMax
                    });


                    if (_burningTimer > _burningRecipeSO._burningTimeMax)
                    {
                        //Если значение больше, то блюдо прожарилось
                        GetKitchenObject().DestroyObject(); //Берем объект и уничтожаем его

                        LinkKitchenObject.SpawnKitchenObject(_burningRecipeSO.output, this); //Создание нового объекта

                        _state = State.Burned; //Переключение на сгоревший объект

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            _state = _state
                        });


                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            _progressNormalized = 0f
                        });

                    }
                    break; */
                case State.Burned:
                    break;
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

                    _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.Frying; //Переключение состояния плиты на прожарку
                    _fryingTimer = 0f; //Сброс таймера

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        _state = _state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        _progressNormalized = _fryingTimer / _fryingRecipeSO._fryingTimerMax
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
        FryingRecipeSO _fryingRecipeSO = GetFryingRecipeSOWithInput(_inputKitchenObjectSO);
        return _fryingRecipeSO != null;
    }

    private KitchenObject GetOutputForInput(KitchenObject _inputKitchenObjectSO)
    {
        FryingRecipeSO _fryingRecipeSO = GetFryingRecipeSOWithInput(_inputKitchenObjectSO);
        if (_fryingRecipeSO != null)
        {
            return _fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObject _inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO _fryingRecipeSO in _fryingRecipeSOArray)
        {
            if (_fryingRecipeSO.input == _inputKitchenObjectSO)
            {
                return _fryingRecipeSO;
            }
        }
        return null;
    }

   /* private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObject _inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO _burningRecipeSO in _burningRecipeSOArray)
        {
            if (_burningRecipeSO.input == _inputKitchenObjectSO)
            {
                return _burningRecipeSO;
            }
        }
        return null;
    }*/

    public bool IsFried()
    {
        return _state == State.Fried;
    }
}
