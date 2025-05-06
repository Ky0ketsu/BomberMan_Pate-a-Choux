using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Scr_Block_Falling : MonoBehaviour
{
    public float time;
    public GameObject colliders;
    public GameObject block;
    bool fallen = false;

    public void Awake()
    {
        block.SetActive(false);
    }

    public void fallBlock()
    {
        if (fallen) return;
        fallen = true;
        Debug.Log("coucou");
        block.SetActive(true);
        block.transform.DOMove(new Vector3(transform.position.x, 0, transform.position.z), time).SetEase(Ease.InExpo).OnComplete(DelayToDisable);
    }

    private void DelayToDisable()
    {
       //colliders.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>())
        {
            other.GetComponent<Scr_Player_Death>().Death();
            other.GetComponent<Scr_Player_Death>().FallingDeath();
        }
    }
}
