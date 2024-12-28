using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; //��������� ����������� ���������
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State _state;
    }
    public enum State
    {
        Idle, //��������� �������� �����
        Frying, //������ ������� �� �����
        Fried, //������ �����, �� ����������� ��������
        Burned, //��������� ������
    }

    // ������� �����

    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSOArray;


    private State _state;

    private float _fryingTimer; //������ ����� ����
    private FryingRecipeSO _fryingRecipeSO;
    private float _burningTimer; //������ �������
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
                        //���� �������� ������, �� ����� �����������
                        GetKitchenObject().DestroyObject(); //����� ������ � ���������� ���

                        LinkKitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this); //�������� ������ �������

                        _state = State.Burned; //������������ ��������� �����, ��� ������ ������������, �� ���������� ��������
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
                        //���� �������� ������, �� ����� �����������
                        GetKitchenObject().DestroyObject(); //����� ������ � ���������� ���

                        LinkKitchenObject.SpawnKitchenObject(_burningRecipeSO.output, this); //�������� ������ �������

                        _state = State.Burned; //������������ �� ��������� ������

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
            //������ ������ ���
            if (_player.HasKitchenObject())
            {
                //  �������� ���-�� ������ � �����
                if (HasRecipeWithInput(_player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //���������� �� ��� ����� ��������
                    _player.GetKitchenObject().SetKitchenObjectParent(this);

                    _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.Frying; //������������ ��������� ����� �� ��������
                    _fryingTimer = 0f; //����� �������

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
                //� ������ ������ ���
            }
        }
        else
        {
            //���� �������� ������
            if (_player.HasKitchenObject())
            {
                //� ������ ���-�� ����
                if (_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject _plateKitchenObject)) //���� ���� ������� � �����
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
                //� ������ ������ ��� � �� ��������� �� ��� ����� �� �������
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
