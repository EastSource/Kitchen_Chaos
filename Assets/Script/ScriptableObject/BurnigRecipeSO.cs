using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurnigRecipeSO : ScriptableObject
{
    public KitchinObjectSO input;
    public KitchinObjectSO output;
    public float burningTimerMax;
}
