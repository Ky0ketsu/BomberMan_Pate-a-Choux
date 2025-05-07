using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_Caramel : MonoBehaviour
{
    [SerializeField] private float timeEffect;


    private bool waitToEffect;
    private void Update()
    {
        if (waitToEffect && GetComponent<Scr_PowerUp_Default>().player != null)
        {
            GetComponent<Scr_PowerUp_Default>().player.GetComponent<Scr_Player_Bomb>().frozenActive = true;
            waitToEffect = false;
            StartCoroutine(TimeEffect());
        }
    }

    IEnumerator TimeEffect()
    {
        yield return new WaitForSeconds(timeEffect);
        GetComponent<Scr_PowerUp_Default>().GetComponent<Scr_Player_Bomb>().caramelActive = false;
        Destroy(gameObject);
    }
}
