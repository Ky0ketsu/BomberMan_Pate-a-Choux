using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scr_PowerUp_Spawn_Animation : MonoBehaviour
{
    public float animationDelay;
    public float animationTime;
    [SerializeField]GameObject particule;

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
        Instantiate(particule, transform.position, transform.rotation, GameObject.Find("ParticuleParent").transform);
        transform.DORotate(transform.eulerAngles + new Vector3(0, 360, 0), animationTime).SetEase(Ease.InOutExpo);
        transform.DOScale(Vector3.one, animationTime).SetEase(Ease.InBounce);
    }
}
