using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Menu_Lobby_PlayerAnimation : MonoBehaviour
{
    public GameObject player;
    public float time;
    public Vector3 startScale;

    public void Start()
    {
        startScale = player.transform.localScale;
        player.transform.localScale = Vector3.zero;
    }

    public void AnimationSpawn()
    {
        player.transform.DOScale(startScale, time).SetEase(Ease.OutBounce);
    }

    public void AnimationDespawn()
    {
        player.transform.DOScale(Vector3.zero, time).SetEase(Ease.InBounce);
    }
}
