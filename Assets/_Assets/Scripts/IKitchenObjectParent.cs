using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenOjectFollowTransform();
    public void SetKitchenObject(LinkKitchenObject _onkitchenObjects);
    public LinkKitchenObject GetKitchenObject();

    public void ClearKitchenObject();
    public bool HasKitchenObject();
}
