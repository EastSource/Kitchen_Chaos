using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]public struct KitchenObjectSO_GameObject
    {
        public KitchinObjectSO kitchinObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchinObject platekitchinObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    public void Start()
    {
        platekitchinObject.OnIngredientAdded += PlatekitchinObject_OnIngredientAdded;
    }

    private void PlatekitchinObject_OnIngredientAdded(object sender, PlateKitchinObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchinObjectSO == e.KitchinObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
