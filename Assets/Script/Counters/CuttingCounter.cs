using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProcess
{
    public event EventHandler<IHasProcess.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    [SerializeField]private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithhInput(player.GetKitchenObject().GetKitchinObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchinObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProcess.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax});
                }
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
                OnProgressChanged?.Invoke(this, new IHasProcess.OnProgressChangedEventArgs(){progressNormalized = 0f});

            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchinObject plateKitchinObject))
                {
                    if (plateKitchinObject.TryAddIngredient(GetKitchenObject().GetKitchinObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithhInput(GetKitchenObject().GetKitchinObjectSO()))
        {
            //There is a kitchenObject here

            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchinObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProcess.OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax});
            OnCut?.Invoke(this, EventArgs.Empty);
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchinObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchinObjectSO());

                GetKitchenObject().DestroySelf();

                KitchinObject.SpawnKitchinObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithhInput(KitchinObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchinObjectSO GetOutputForInput(KitchinObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchinObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
    
}
