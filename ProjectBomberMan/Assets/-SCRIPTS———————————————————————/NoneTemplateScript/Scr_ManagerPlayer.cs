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
    public bool[] players = new bool[4];
    public GameObject playerSlot;


    void Awake()
    {
        EVENTS.OnLobby += EnablePlayerAdd;
        EVENTS.OnLobbyExit += DisablePlayerAdd;
        EVENTS.OnVictory += DespawnPlayer;
        EVENTS.OnGameStart +=
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

    private void Start()
    {
        foreach(Player player in ReInput.players.GetPlayers())
        {
            //Debug.Log(player.controllers.joystickCount);
        }
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
        Player myPlayer = ReInput.players.GetPlayer(player);
        if (myPlayer.GetButtonDown("Join"))
        {
            if (players[player] == false)
            {
                myPlayer.isPlaying = true;
                players[player] = true;
                playerSlot.transform.GetChild(player).GetComponent<Scr_Menu_Lobby_PlayerAnimation>().AnimationSpawn();
                return;
            }
        }
        if (myPlayer.GetButtonDown("Quit"))
        {
            if (players[player] == true)
            {
                myPlayer.isPlaying = false;
                players[player] = true;
                playerSlot.transform.GetChild(player).GetComponent<Scr_Menu_Lobby_PlayerAnimation>().AnimationDespawn();
            }
        }
    }

    private void Spawn()
    {

    }

    private void FixedUpdate()
    {
        if(isPlay) CheckPlayerAlive();
    }

    void CheckPlayerAlive()
    {
        foreach(bool players in players)
        {
            Debug.Log(players);
        }
    }

    private void DespawnPlayer()
    {
        
    }
}
