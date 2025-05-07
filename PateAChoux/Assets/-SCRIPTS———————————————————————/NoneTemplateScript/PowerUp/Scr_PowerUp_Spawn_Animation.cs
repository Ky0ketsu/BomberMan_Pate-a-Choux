using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scr_PowerUp_Spawn_Animation : MonoBehaviour
{
    public float animationDelay;
    public float animationTime;

    public void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(animationDelay);
        ReScale();
    }

    public void ReScale()
    {
        transform.DORotate(new Vector3(0, 360, 0), animationTime).SetEase(Ease.InCubic);
        transform.DOScale(Vector3.one, animationTime).SetEase(Ease.InBounce);
    }
}
