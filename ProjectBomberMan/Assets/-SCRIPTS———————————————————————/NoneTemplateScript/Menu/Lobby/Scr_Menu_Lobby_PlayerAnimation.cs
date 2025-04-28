using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Menu_Lobby_PlayerAnimation : MonoBehaviour
{
    public GameObject player;
    public float time;

    public void Start()
    {
        player.transform.localScale = Vector3.zero;
    }

    public void AnimationSpawn()
    {
        player.transform.DOScale(Vector3.one, time).SetEase(Ease.OutBounce);
    }

    public void AnimationDespawn()
    {
        player.transform.DOScale(Vector3.zero, time).SetEase(Ease.InBounce);
    }
}
