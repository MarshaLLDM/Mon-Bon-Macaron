using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class BlenderCounter : BaseCounter, IHasProgress
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
    }

    // ������� �����

    [SerializeField] private BlenderRecipeSO[] _blenderRecipeSOArray;


    private State _state;

    private float _fryingTimer; //������ ����� ����
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
                        //���� �������� ������, �� ����� �����������
                        GetKitchenObject().DestroyObject(); //����� ������ � ���������� ���

                        LinkKitchenObject.SpawnKitchenObject(_blenderRecipeSO.output, this); //�������� ������ �������

                        _state = State.Idle; //������������ ��������� �����, ��� ������ ������������, �� ���������� ��������
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
            //������ ������ ���
            if (_player.HasKitchenObject())
            {
                //  �������� ���-�� ������ � �����
                if (HasRecipeWithInput(_player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //���������� �� ��� ����� ��������
                    _player.GetKitchenObject().SetKitchenObjectParent(this);

                    _blenderRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.Frying; //������������ ��������� ����� �� ��������
                    _fryingTimer = 0f; //����� �������

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
