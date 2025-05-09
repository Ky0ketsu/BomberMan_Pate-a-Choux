using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Block_FallingTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("ecraser");

        if(other.GetComponentInParent<PlayerMove>())
        {
            other.GetComponentInParent<Scr_Player_Death>().Death();
            other.GetComponentInParent<Scr_Player_Death>().FallingDeath();
        }

        if (other.GetComponentInParent<Scr_Block_Breakable>()) other.GetComponentInParent<Scr_Block_Breakable>().Explode(false);
    }
}
