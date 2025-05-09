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
        player.transform.DOScale(playerStartScale, time).SetEase(Ease.OutBounce);
    }

    public void AnimationDespawn()
    {
        controller.transform.DOScale(playerStartScale, time).SetEase(Ease.OutBounce);
        player.transform.DOScale(Vector3.zero, time).SetEase(Ease.InBounce);
    }
}
