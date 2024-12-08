using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchinObjectSO input;
    public KitchinObjectSO output;
    public int cuttingProgressMax;
}
