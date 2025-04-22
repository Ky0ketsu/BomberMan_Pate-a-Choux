using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Bomb : MonoBehaviour
{
    public GameObject owner;
    public float timer;
    public float maxY, minY;


    public void Start()
    {
        StartCoroutine(Timer());
        transform.DOMoveY(maxY, 0.5f).SetEase(Ease.OutExpo);
        transform.DOMoveY(minY, 1).SetEase(Ease.InExpo);
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Explosion();
    }

    public void Explosion()
    {
        if(owner != null)
        {
            owner.GetComponent<Scr_Player_Bomb>().stockBomb++;
        }

        gameObject.GetComponent<Scr_Bomb_Propagation>().Explosion();

        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
