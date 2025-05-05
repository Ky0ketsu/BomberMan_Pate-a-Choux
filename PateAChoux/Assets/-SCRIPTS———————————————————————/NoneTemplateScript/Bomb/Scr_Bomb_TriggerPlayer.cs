using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bomb_TriggerPlayer : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerMove>())
        {
            var player = other.GetComponentInParent<Scr_Player_Death>();
            player.BombDeath();
            player.Death();
        }
    }
}
