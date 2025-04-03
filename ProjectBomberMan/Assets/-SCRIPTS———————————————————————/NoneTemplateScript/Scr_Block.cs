using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Block : MonoBehaviour
{
    public bool canDestroy;


    public void Explode()
    {
        if(canDestroy)
        {
            Destroy(gameObject);
        }
    }
}
