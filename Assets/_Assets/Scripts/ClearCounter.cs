using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObject _kitchenObject;

    public override void Interact(Player _player)
    {
        if (!HasKitchenObject())
        {
            //Значит ничего нет
            if(_player.HasKitchenObject()){
              //  Персонаж что-то держит в руках
              _player.GetKitchenObject().SetKitchenObjectParent(this);
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
                if(_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject _plateKitchenObject)) //Если есть тарелка в руках
                {
                    if (_plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroyObject();
                    }
                }
                else
                {
                    //Игрок несет не тарелку, а другой объект
                    if(GetKitchenObject().TryGetPlate(out _plateKitchenObject)) //Проверка стола
                    {
                        //Значит на стойке стоит тарелка
                        if (_plateKitchenObject.TryAddIngredient(_player.GetKitchenObject().GetKitchenObjectSO())) //Переходим к тарелке и пытаемся добавить ингредиент, который у игрока
                        {
                            _player.GetKitchenObject().DestroyObject(); //Уничтожение ингридиента, который у игрока
                        }
                    }
                }
            }
            else
            {
                //У игрока ничего нет и мы подбираем то что лежит на объекте
                GetKitchenObject().SetKitchenObjectParent(_player); 
            }
        }
    }
}
