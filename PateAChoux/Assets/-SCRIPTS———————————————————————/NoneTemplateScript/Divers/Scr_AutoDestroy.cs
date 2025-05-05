using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_AutoDestroy : MonoBehaviour
{
    public float time;

    public void Start()
    {
        StartCoroutine(DelayToDestroy());
    }

    IEnumerator DelayToDestroy()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
