using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StoveCounter : BaseCounter, IHasProcess
{
    public event EventHandler<IHasProcess.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnstateChangedEventArgs> OnstateChanged;

    public class OnstateChangedEventArgs : EventArgs
    {
        public State state;
    }
    
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurnigRecipeSO[] burningRecipeSOArray;
    private float fryingTimer;
    private float burningTimer;
    private State state;
    private FryingRecipeSO fryingRecipeSO;
    private BurnigRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }
    
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProcess.OnProgressChangedEventArgs(){progressNormalized = fryingTimer / fryingRecipeSO.flyingTimerMax});
                    if (fryingTimer > fryingRecipeSO.flyingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchinObject.SpawnKitchinObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchinObjectSO());
                        OnstateChanged?.Invoke(this, new OnstateChangedEventArgs() { state = state });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProcess.OnProgressChangedEventArgs(){progressNormalized = burningTimer / burningRecipeSO.burningTimerMax});
                    if (burningTimer > burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchinObject.SpawnKitchinObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnstateChanged?.Invoke(this, new OnstateChangedEventArgs() { state = state });

                    }
                    break;
                case State.Burned:
                    OnProgressChanged?.Invoke(this, new IHasProcess.OnProgressChangedEventArgs(){progressNormalized = 0f});

                    OnstateChanged?.Invoke(this, new OnstateChangedEventArgs() { state = state });
                    break;
            }
        }

    }
    
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
                    
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchinObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnstateChanged?.Invoke(this, new OnstateChangedEventArgs() { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProcess.OnProgressChangedEventArgs(){progressNormalized = fryingTimer / fryingRecipeSO.flyingTimerMax});

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
                state = State.Idle;
                OnstateChanged?.Invoke(this, new OnstateChangedEventArgs() { state = state });
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
                
                state = State.Idle;
                OnstateChanged?.Invoke(this, new OnstateChangedEventArgs() { state = state });
                OnProgressChanged?.Invoke(this, new IHasProcess.OnProgressChangedEventArgs(){progressNormalized = 0f});
            }
        }
    }
    
    private bool HasRecipeWithhInput(KitchinObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchinObjectSO GetOutputForInput(KitchinObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchinObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurnigRecipeSO GetBurningRecipeSOWithInput(KitchinObjectSO inputKitchenObjectSO)
    {
        foreach (BurnigRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

}
