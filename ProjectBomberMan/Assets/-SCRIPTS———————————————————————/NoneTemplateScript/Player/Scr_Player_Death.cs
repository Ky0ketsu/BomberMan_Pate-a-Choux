using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scr_Player_Death : MonoBehaviour
{
    public Transform graphic;
    public Transform colliders;

    private Transform UnActivePlayerParent;

    /*public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FallingBlock"))
        {
            Death();
            FallingDeath();
        }

        if(other.CompareTag("Bomb"))
        {
            Death();
            BombDeath();
        }
    }*/

    public void FallingDeath()
    {
        graphic.DOScale(new Vector3(1.3f, 0.02f, 1.3f), 0.2f);
    }

    public void BombDeath()
    {
        graphic.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce);
    }

    public void Death()
    {
        transform.GetComponent<PlayerMove>().CanRun = false;
        colliders.gameObject.SetActive(false);
        GetComponent<CharacterController>().enabled = false;
        transform.parent = GameObject.Find("UnActivePlayerParent").transform;
    }
}
