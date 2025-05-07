using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_RangeUp : MonoBehaviour
{
    private bool waitToEffect;
    private void Update()
    {
        if (waitToEffect && GetComponent<Scr_PowerUp_Default>().player != null)
        {
            GetComponent<Scr_PowerUp_Default>().player.GetComponent<PlayerMove>().speedUp++;
            waitToEffect = false;
        }

        if(waitToEffect && GetComponent<Scr_PowerUp_Default>().frozen != null)
        {
            GetComponent<Scr_PowerUp_Default>().frozen.GetComponent<Scr_Bomb_Propagation>().range++;
        }
    }
}
