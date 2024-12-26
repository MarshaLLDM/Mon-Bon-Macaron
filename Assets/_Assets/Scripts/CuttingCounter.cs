using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress

    //�������� ����

{
    public static event EventHandler OnAnyCut; //������� ��� ��������������� �����

    new public static void ResetStartStatic()
    {
        OnAnyCut = null; // ����� ��������
    }

    public event EventHandler <IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; //��������� ����������� ���������

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;

    private int _cuttingProgress;
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
                    _cuttingProgress = 0;

                    CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        _progressNormalized = (float)_cuttingProgress / _cuttingRecipeSO._cuttingProgressMax
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
                    }
                }
            }
            else
            {
                //� ������ ������ ��� � �� ��������� �� ��� ����� �� �������
                GetKitchenObject().SetKitchenObjectParent(_player);

               /* OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    _progressNormalized = 0f
                });*/
            }
        }
    }

    public override void InteractAlternate(Player _player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //����� ����� ������ � ������ ������� � ���� ����� ������
            _cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty); //����� ������� ��� ��������������� �����

            CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                _progressNormalized = (float)_cuttingProgress / _cuttingRecipeSO._cuttingProgressMax
            });

            if (_cuttingProgress >= _cuttingRecipeSO._cuttingProgressMax)
            {
                KitchenObject _outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroyObject();
                LinkKitchenObject.SpawnKitchenObject(_outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObject _inputKitchenObjectSO)
    {
        CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeSOWithInput(_inputKitchenObjectSO);
        return _cuttingRecipeSO != null;
    }

    private KitchenObject GetOutputForInput(KitchenObject _inputKitchenObjectSO)
    {
        CuttingRecipeSO _cuttingRecipeSO = GetCuttingRecipeSOWithInput (_inputKitchenObjectSO);
        if (_cuttingRecipeSO != null)
        {
            return _cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
     }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObject _inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO _cuttingRecipeSO in _cuttingRecipeSOArray)
        {
            if (_cuttingRecipeSO.input == _inputKitchenObjectSO)
            {
                return _cuttingRecipeSO;
            }
        }
        return null;
    }
}
