using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using Unity.VisualScripting;

public class Scr_MortSubite : MonoBehaviour
{
    [SerializeField] float timer;

    private void Awake()
    {
        EVENTS.OnGameStart += StartTimer;
    }
    private void OnDestroy()
    {
        EVENTS.OnGameStart -= StartTimer;
    }

    public void StartTimer()
    {
        Invoke("StartMortSubite", timer);
    }

    public void StartMortSubite()
    {
        EVENTS.InvokeMortSubite();
    }

}
