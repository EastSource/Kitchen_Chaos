using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchinObject : KitchinObject
{
   public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

   public class OnIngredientAddedEventArgs : EventArgs
   {
      public KitchinObjectSO KitchinObjectSO;
   }
   
   [SerializeField] private List<KitchinObjectSO> validKitchenObjectSOList;
   private List<KitchinObjectSO> kitchenObjectSOList;

   private void Awake()
   {
      kitchenObjectSOList = new List<KitchinObjectSO>();
   }

   public bool TryAddIngredient(KitchinObjectSO kitchinObjectSO)
   {
      if (!validKitchenObjectSOList.Contains(kitchinObjectSO))
      {
         return  false;
      }
      if (kitchenObjectSOList.Contains(kitchinObjectSO))
      {
         //Already has this type
         return false;
      }
      else
      {
         kitchenObjectSOList.Add(kitchinObjectSO);
         
         OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { KitchinObjectSO = kitchinObjectSO });
         return true;
      }
   }
}
