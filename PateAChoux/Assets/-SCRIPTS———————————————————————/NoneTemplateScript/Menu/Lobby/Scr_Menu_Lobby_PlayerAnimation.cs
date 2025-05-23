using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Menu_Lobby_PlayerAnimation : MonoBehaviour
{
    public GameObject player;
    public GameObject controller;
    public GameObject ready;
    public float time;

    public void Start()
    {

        player.transform.localScale = Vector3.zero;
    }

    public void AnimationSpawn()
    {
        controller.transform.DOScale(Vector3.zero, time).SetEase(Ease.InBounce);
        ready.transform.DOScale(Vector3.one, time).SetEase(Ease.OutBounce);
        player.transform.DOScale(Vector3.one, time).SetEase(Ease.OutBounce).OnComplete(StartYoyo);
    }

    void StartYoyo()
    {
       player.transform.DOScale(Vector3.one * 1.1f, time / 2).SetEase(Ease.Linear).SetLoops(-1 ,LoopType.Yoyo);
    }

    public void AnimationDespawn()
    {
        player.transform.DOKill();
        controller.transform.DOScale(Vector3.one, 0.1f);
        player.transform.DOScale(Vector3.zero, 0.1f);
        ready.transform.DOScale(Vector3.zero, 0.1f);
    }
}
