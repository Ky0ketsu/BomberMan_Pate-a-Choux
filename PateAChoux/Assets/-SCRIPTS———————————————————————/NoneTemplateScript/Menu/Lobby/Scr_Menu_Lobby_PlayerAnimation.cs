using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Menu_Lobby_PlayerAnimation : MonoBehaviour
{
    public GameObject player;
    public GameObject controller;
    public float time;
    public Vector3 playerStartScale, controllerStartScale;

    public void Start()
    {
        controllerStartScale = controller.transform.localScale;
        playerStartScale = player.transform.localScale;
        player.transform.localScale = Vector3.zero;
    }

    public void AnimationSpawn()
    {
        controller.transform.DOScale(Vector3.zero, time).SetEase(Ease.InBounce);
        player.transform.DOScale(playerStartScale, time).SetEase(Ease.OutBounce).OnComplete(StartYoyo);
    }

    void StartYoyo()
    {
        player.transform.DORotate(new Vector3(0, 0, player.transform.rotation.z), time).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        player.transform.DOScale(playerStartScale * 1.1f, time / 2).SetEase(Ease.Linear).SetLoops(-1 ,LoopType.Yoyo);
    }

    void StopYoyo()
    {

    }

    public void AnimationDespawn()
    {
        controller.transform.DOScale(playerStartScale, time).SetEase(Ease.OutBounce);
        player.transform.DOScale(Vector3.zero, time).SetEase(Ease.InBounce);
    }
}
