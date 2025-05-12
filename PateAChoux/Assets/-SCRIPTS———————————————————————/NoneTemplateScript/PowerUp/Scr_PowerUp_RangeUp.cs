using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_RangeUp : MonoBehaviour
{
    private bool effectUse = false;

    private void Update()
    {
        if (!effectUse && GetComponent<Scr_PowerUp_Default>().player != null)
        {
            GetComponent<Scr_PowerUp_Default>().player.GetComponent<Scr_Player_Bomb>().range++;
            effectUse = true;
        }

        if(!effectUse && GetComponent<Scr_PowerUp_Default>().frozen != null)
        {
            GetComponent<Scr_PowerUp_Default>().frozen.GetComponent<Scr_Bomb_Propagation>().range++;
            effectUse = true;
        }
    }
}
