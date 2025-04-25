using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bomb_Caramel : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if(other.GetComponentInParent<Scr_Player_Bomb>() != null)
        {
            other.GetComponentInParent<PlayerMove>().maxSpeed = other.GetComponentInParent<PlayerMove>().startMaxSpeed * 0.6f;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.GetComponentInParent<Scr_Player_Bomb>() != null)
        {
            other.GetComponentInParent<PlayerMove>().maxSpeed = other.GetComponentInParent<PlayerMove>().startMaxSpeed;
        }
    }
}
