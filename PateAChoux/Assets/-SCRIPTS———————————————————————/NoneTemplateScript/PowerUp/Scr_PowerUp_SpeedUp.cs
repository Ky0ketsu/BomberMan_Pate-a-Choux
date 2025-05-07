using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_SpeedUp : MonoBehaviour
{
    private bool waitToEffect;
    private void Update()
    {
        if (waitToEffect && GetComponent<Scr_PowerUp_Default>().player != null)
        {
            GetComponent<Scr_PowerUp_Default>().player.GetComponent<PlayerMove>().speedUp++;
            waitToEffect = false;
        }
    }
}
