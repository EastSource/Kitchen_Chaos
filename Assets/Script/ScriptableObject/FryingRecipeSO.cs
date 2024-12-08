using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchinObjectSO input;
    public KitchinObjectSO output;
    public float flyingTimerMax;
}
