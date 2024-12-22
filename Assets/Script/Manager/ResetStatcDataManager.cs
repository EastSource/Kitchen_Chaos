using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatcDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData(); 
    }
}
