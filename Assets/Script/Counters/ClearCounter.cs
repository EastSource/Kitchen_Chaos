using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]private KitchinObjectSO kitchinObjectSO;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carrying anything
            }
        }
        else
        {
            //There is a KitchenObject here
            if (!player.HasKitchenObject())
            {
                //Player is not carrying anything
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                
            }
        }
    }
    
}
