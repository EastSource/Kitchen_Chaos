using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    private void Start()
    {
        Player.Instance.OnSeleCtedCounterChanged += Player_OnSelectedcounterChanged;
    }

    private void Player_OnSelectedcounterChanged(object sender, Player.OnSelectedCounterChanvedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}