using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Scr_ManagerPlayer : MonoBehaviour
{
    [SerializeField] private bool canInput;
    [SerializeField] private bool isPlay;

    public GameObject playerPrefab;
    private GameObject[] players = new GameObject[4];
    public GameObject playerSlot;


    void Awake()
    {
        EVENTS.OnLobby += EnablePlayerAdd;
        EVENTS.OnLobbyExit += DisablePlayerAdd;
        EVENTS.OnVictory += DespawnPlayer;
    }

    void OnDestroy()
    {
        EVENTS.OnLobby -= EnablePlayerAdd;
        EVENTS.OnLobbyExit -= DisablePlayerAdd;
        EVENTS.OnVictory -= DespawnPlayer;
    }

    void EnablePlayerAdd()
    {
        canInput = true;
        playerSlot.transform.GetChild(0).GetComponent<Scr_Menu_Lobby_PlayerAnimation>().AnimationSpawn();
    }

    void DisablePlayerAdd()
    {
        canInput = false;
    }

    

    private void Update()
    {
        if(canInput)
        {
            JoinOrQuit(1);
            JoinOrQuit(2);
            JoinOrQuit(3);
        }
    }

    void JoinOrQuit(int player)
    {
        if (ReInput.players.GetPlayer(player).GetButtonDown("Join"))
        {
            if (players[player] == null)
            {
                players[player] = Instantiate(playerPrefab, GameObject.Find("PlayerParent").transform);
                players[player].GetComponent<PlayerMove>().playerID = player;
                playerSlot.transform.GetChild(player).GetComponent<Scr_Menu_Lobby_PlayerAnimation>().AnimationSpawn();
            }
        }
        if (ReInput.players.GetPlayer(player).GetButtonDown("Quit"))
        {
            if (players[player] != null)
            {
                Destroy(players[player]);
                playerSlot.transform.GetChild(player).GetComponent<Scr_Menu_Lobby_PlayerAnimation>().AnimationDespawn();
            }
        }
    }
    
    private void DespawnPlayer()
    {
        
    }
}
