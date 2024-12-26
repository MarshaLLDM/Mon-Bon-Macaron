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
            //������ ������ ���
            if(_player.HasKitchenObject()){
              //  �������� ���-�� ������ � �����
              _player.GetKitchenObject().SetKitchenObjectParent(this);
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
                if(_player.GetKitchenObject().TryGetPlate(out PlateKitchenObject _plateKitchenObject)) //���� ���� ������� � �����
                {
                    if (_plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroyObject();
                    }
                }
                else
                {
                    //����� ����� �� �������, � ������ ������
                    if(GetKitchenObject().TryGetPlate(out _plateKitchenObject)) //�������� �����
                    {
                        //������ �� ������ ����� �������
                        if (_plateKitchenObject.TryAddIngredient(_player.GetKitchenObject().GetKitchenObjectSO())) //��������� � ������� � �������� �������� ����������, ������� � ������
                        {
                            _player.GetKitchenObject().DestroyObject(); //����������� �����������, ������� � ������
                        }
                    }
                }
            }
            else
            {
                //� ������ ������ ��� � �� ��������� �� ��� ����� �� �������
                GetKitchenObject().SetKitchenObjectParent(_player); 
            }
        }
    }
}
