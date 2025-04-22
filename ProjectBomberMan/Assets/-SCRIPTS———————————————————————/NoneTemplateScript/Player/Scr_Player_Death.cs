using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scr_Player_Death : MonoBehaviour
{
    public Transform graphic;
    public float time;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FallingBlock"))
        {
            FallingDeath();
        }
    }

    private void FallingDeath()
    {
        transform.GetComponent<PlayerMove>().CanRun = false;
        graphic.DOScale(new Vector3(1.3f, 0.2f, 0), time);
    }
}
