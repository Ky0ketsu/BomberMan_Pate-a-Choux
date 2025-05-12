using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class Scr_PowerUp_BombeUp : MonoBehaviour
{
    private bool effectUse = false;
    private void Update()
    {
        if(!effectUse && GetComponent<Scr_PowerUp_Default>().player != null)
        {
            GetComponent<Scr_PowerUp_Default>().player.GetComponent<Scr_Player_Bomb>().stockBomb++;
            effectUse = true;
        }
    }
}
