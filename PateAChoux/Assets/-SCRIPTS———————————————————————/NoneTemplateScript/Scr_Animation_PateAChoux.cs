using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Animation_PateAChoux : MonoBehaviour
{
    [SerializeField] private GameObject pateAChoux, rightArm, leftArm, body;

    [HideInInspector] private GameObject[] players;

    private void Start()
    {
        EVENTS.OnStartAnimation += playerSpawnAnimation;
    }

    private void OnDestroy()
    {
        EVENTS.OnStartAnimation -= playerSpawnAnimation;
    }


    public void playerSpawnAnimation()
    {
        for (int i = 0; i < Scr_ManagerPlayer.acces.activePlayers.Length; i++)
        {
            if (Scr_ManagerPlayer.acces.activePlayers[i] != null)
            {

            }
        }
    }

    private void SpawnSequence()
    {

    }
}
