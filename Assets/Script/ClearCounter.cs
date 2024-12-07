using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField]private KitchinObjectSO kitchinObjectSO;
    [SerializeField] private Transform counterTopPoint;
    
    private KitchinObject kitchenObject;
    
    public void Interact(Player player)
    {
        
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchinObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchinObject>().SetKitchenObjectParent(this);
        }
        else
        {
            // Give the object to player
             kitchenObject.SetKitchenObjectParent(player);
            Debug.Log(kitchenObject.GetKicthenObjectParent());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchinObject kitchinObject)
    {
        this.kitchenObject = kitchinObject;
    }

    public KitchinObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
