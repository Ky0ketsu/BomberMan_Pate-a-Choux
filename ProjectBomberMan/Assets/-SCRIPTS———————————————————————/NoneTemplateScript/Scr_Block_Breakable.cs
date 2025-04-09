using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Block_Breakable : MonoBehaviour
{
    public bool canDestroy;


    public void Explode()
    {
        Destroy(gameObject);
    }
}
