using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();

    public void SetKitchenObject(KitchinObject kitchinObject);

    public KitchinObject GetKitchenObject();

    public void ClearKitchenObject();
    
    public bool HasKitchenObject();
}
