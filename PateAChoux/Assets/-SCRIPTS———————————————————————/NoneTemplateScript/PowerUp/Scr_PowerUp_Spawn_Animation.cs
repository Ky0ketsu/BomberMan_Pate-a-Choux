using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scr_PowerUp_Spawn_Animation : MonoBehaviour
{
    public float animationDelay;
    public float animationTime;
    [SerializeField]GameObject particule;
    [SerializeField]Transform graphics;

    public void Start()
    {
        graphics.localScale = Vector3.zero;
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(animationDelay);
        ReScale();
    }

    public void ReScale()
    {
        Instantiate(particule, transform.position, transform.rotation, GameObject.Find("ParticuleParent").transform);
        graphics.DORotate(transform.eulerAngles + new Vector3(0, 360, 0), animationTime).SetEase(Ease.InOutExpo);
        graphics.DOScale(Vector3.one, animationTime).SetEase(Ease.InBounce);

        StartCoroutine(AnimationLoop());
        
    }

    void AnimationLoopFonction()
    {
        graphics.DOKill();
        StartCoroutine(AnimationLoop());
    }

    IEnumerator AnimationLoop()
    {
        graphics.DOMoveY(transform.position.y + 3, 0.8f).SetEase(Ease.InBounce);
        graphics.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        graphics.DOScale(new Vector3(0.8f, 1.2f, 0.8f), 0.4f);
        yield return new WaitForSeconds(0.4f);
        graphics.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        graphics.DOMoveY(transform.position.y, 0.8f).SetEase(Ease.InCubic);
        graphics.DOScale(new Vector3(0.8f, 1.2f, 0.8f), 0.6f);
        yield return new WaitForSeconds(0.6f);
        graphics.DOScale(new Vector3(1.2f, 0.8f, 1.2f), 0.2f);
        yield return new WaitForSeconds(0.2f);
        graphics.DOScale(Vector3.one, 0.2f);
        yield return new WaitForSeconds(0.2f);
        graphics.DOScale(Vector3.one * 1.2f, 1).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(3);
        AnimationLoopFonction();
    }
}
