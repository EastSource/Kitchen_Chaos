using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchinObject : MonoBehaviour
{
    [SerializeField] private KitchinObjectSO KitchinObjectSO;
    
    private IKitchenObjectParent kitchenObjectParent;

    public KitchinObjectSO GetKitchinObjectSO()
    {
        return KitchinObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent/*移動先のkitchenObjectParent*/)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        if (this.kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already exists");
        } 
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKicthenObjectParent()
    {
        return kitchenObjectParent;
    }
}