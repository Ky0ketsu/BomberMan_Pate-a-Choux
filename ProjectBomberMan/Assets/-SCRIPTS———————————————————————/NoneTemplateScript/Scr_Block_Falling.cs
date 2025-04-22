using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Scr_Block_Falling : MonoBehaviour
{
    public float time;
    public GameObject colliders;

    void Start()
    {
        transform.DOMove(new Vector3(transform.position.x, 0, transform.position.z), time).SetEase(Ease.InExpo);
        StartCoroutine(DelayToDisable());
    }

    IEnumerator DelayToDisable()
    {
        yield return new WaitForSeconds(time);
        colliders.SetActive(false);
    }

    
}
