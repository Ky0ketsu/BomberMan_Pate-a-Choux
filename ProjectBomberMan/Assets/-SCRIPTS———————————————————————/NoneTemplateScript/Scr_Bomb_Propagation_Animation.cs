using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Scr_Bomb_Propagation_Animation : MonoBehaviour
{
    public float animationTime;

    public void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, animationTime).SetEase(Ease.InOutExpo).SetLoops(1, LoopType.Yoyo);
    }

}
