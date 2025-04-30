using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System.Linq;

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
        StartCoroutine(DelayToEnable());
    }

    IEnumerator DelayToEnable()
    {
        yield return new WaitForSeconds(1f);
        canInput = true;
    }

    void DisablePlayerAdd()
    {
        canInput = false;
    }

    

    private void Update()
    {
        if(canInput)
        {
            JoinOrQuit(0);
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

    private void FixedUpdate()
    {
        if(isPlay) CheckPlayerAlive();
    }

    void CheckPlayerAlive()
    {
        for (int i = 0; i < players.Length; i++)
        {
            GameObject currentPlayerCheck;
            float currentPlayerAlive = 4;
            if (players[i] == null)
            {
                currentPlayerAlive--;
            }
            else
            {
                currentPlayerCheck = players[i];
            }
        }
    }

    private void DespawnPlayer()
    {
        
    }
}
