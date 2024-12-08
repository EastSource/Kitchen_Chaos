using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        stoveCounter.OnstateChanged += StoveCounter_OnstateChanged;
    }

    private void StoveCounter_OnstateChanged(object sender, StoveCounter.OnstateChangedEventArgs e)
    {
        bool showvisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Burned;
        stoveOnGameObject.SetActive(showvisual);
        particlesGameObject.SetActive(showvisual);
    }

}
