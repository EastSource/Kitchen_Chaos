using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] private KitchinObjectSO kitchinObjectSO; 
    
    public override void Interact(Player player)
    {
        
        if (!player.HasKitchenObject())
        {
            KitchinObject.SpawnKitchinObject(kitchinObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
    
    
}
