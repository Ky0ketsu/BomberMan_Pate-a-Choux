using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Bomb : MonoBehaviour
{
    public GameObject owner;
    private Rigidbody rigid;
    public float timer;
    public LayerMask mask;
    public Vector3 offset;

    public Vector3 dir;

    public void Start()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(Timer());
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
            owner.GetComponent<Scr_Player_Bombe>().stockBomb++;
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
