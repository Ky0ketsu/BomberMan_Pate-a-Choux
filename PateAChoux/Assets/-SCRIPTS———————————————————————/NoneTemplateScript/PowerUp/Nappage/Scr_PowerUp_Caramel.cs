using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_Caramel : MonoBehaviour
{
    [SerializeField] private float timeEffect;


    private bool effectUse = false;
    private void Update()
    {
        if (!effectUse && GetComponent<Scr_PowerUp_Default>().player != null)
        {
            GetComponent<Scr_PowerUp_Default>().player.GetComponent<Scr_Player_Bomb>().frozenActive = true;
            effectUse = true;
            StartCoroutine(TimeEffect());
        }
    }

    IEnumerator TimeEffect()
    {
        yield return new WaitForSeconds(timeEffect);
        GetComponent<Scr_PowerUp_Default>().player.GetComponent<Scr_Player_Bomb>().caramelActive = false;
        Destroy(gameObject);
    }
}
