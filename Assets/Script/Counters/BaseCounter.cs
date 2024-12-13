using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    [SerializeField] private Transform counterTopPoint;
    
    private KitchinObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter Interact");
    }
    
    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter Interact");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchinObject kitchinObject)
    {
        this.kitchenObject = kitchinObject;

        if (kitchinObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
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
