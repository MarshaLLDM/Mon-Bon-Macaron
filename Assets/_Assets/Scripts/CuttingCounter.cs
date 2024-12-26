using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress

    //Резочный стол

{
    public static event EventHandler OnAnyCut; //Функция для воспроизведения звука

    new public static void ResetStartStatic()
    {
        OnAnyCut = null; // Сброс подписки
    }

    public event EventHandler <IHasProgress.OnProgressChangedEventArgs> OnProgressChanged; //Обработка визуального прогресса

    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;

    private int _cuttingProgress;
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
                    //Показывает то что можно порезать
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
                    }
                }
            }
            else
            {
                //У игрока ничего нет и мы подбираем то что лежит на объекте
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
            //Резка будет только в случае объекта и если можно резать
            _cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty); //Вызов функции для воспроизведения звука

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
