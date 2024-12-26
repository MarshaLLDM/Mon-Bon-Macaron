using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkKitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObject _kitchenObject;

    private IKitchenObjectParent _kitchenObjectParrent; //что-то лежит на нем

    public KitchenObject GetKitchenObjectSO()
    {
        return _kitchenObject;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent _kitchenObjectParrent)
    {
        if (this._kitchenObjectParrent != null)
        {
            this._kitchenObjectParrent.ClearKitchenObject();
        }

        this._kitchenObjectParrent = _kitchenObjectParrent;

        if (_kitchenObjectParrent.HasKitchenObject()) 
        {
            Debug.LogError("Уже есть объект");
        }

        _kitchenObjectParrent.SetKitchenObject(this);

        transform.parent = _kitchenObjectParrent.GetKitchenOjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return _kitchenObjectParrent;
    }

    public void DestroyObject()
    {
        _kitchenObjectParrent.ClearKitchenObject();//Снятие с родительского и удаление объекта
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject _plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            _plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            _plateKitchenObject = null;
            return false;
        }
    }

    public static LinkKitchenObject SpawnKitchenObject(KitchenObject kitchenObject, IKitchenObjectParent _kitchenObjectParent)
    {
        Transform _kitchenObjectTransform = Instantiate(kitchenObject._prefab);
        LinkKitchenObject _kitchenObject = _kitchenObjectTransform.GetComponent<LinkKitchenObject>();
        _kitchenObject.SetKitchenObjectParent(_kitchenObjectParent);
        return _kitchenObject;
    }
}
