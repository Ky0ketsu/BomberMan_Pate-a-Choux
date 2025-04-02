using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_Bomb : MonoBehaviour
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

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Explosion();
    }

    private void Explosion()
    {
        if(owner != null)
        {
            owner.GetComponent<scr_Player_Bombe>().stockBomb++;
        }

        for(int i = 0; i < 4; i++)
        {
        
        }
    }

    IEnumerator animation()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
