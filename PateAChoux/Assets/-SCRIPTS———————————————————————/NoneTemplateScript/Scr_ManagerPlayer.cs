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
    public GameObject playerSlotMenu;
    private Transform playerParent;

    public Vector3[] playerSpawnPos = new Vector3[4];


    void Awake()
    {
        EVENTS.OnLobby += EnablePlayerAdd;
        EVENTS.OnLobbyExit += DisablePlayerAdd;
        EVENTS.OnVictory += DespawnPlayer;
        EVENTS.OnGameStart += Spawn;
    }

    void OnDestroy()
    {
        EVENTS.OnLobby -= EnablePlayerAdd;
        EVENTS.OnLobbyExit -= DisablePlayerAdd;
        EVENTS.OnVictory -= DespawnPlayer;
        EVENTS.OnGameStart -= Spawn;
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
            Debug.Log(player.controllers.joystickCount);
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
                playerSlotMenu.transform.GetChild(player).GetComponent<Scr_Menu_Lobby_PlayerAnimation>().AnimationSpawn();
                return;
            }
        }
    }

    private void Spawn()
    {
        playerParent = GameObject.Find("ActivePlayerParent").transform;
        for (int i = 0; i < 3; i++)
        {
            if (players[i] == true)
            {
                GameObject currentPlayer = Instantiate(playerPrefab, playerSpawnPos[i],transform.rotation, playerParent);
                currentPlayer.GetComponent<PlayerMove>().playerID = i;
                currentPlayer.name = "Joueur " + (i+1);
            }
        }
        isPlay = true;
    }

    private void FixedUpdate()
    {
        if(isPlay) CheckPlayerAlive();
    }

    void CheckPlayerAlive()
    {
        if(playerParent.childCount == 0)
        {
            GAME.MANAGER.SwitchTo(State.waiting);
            EVENTS.InvokeDefeat();
            isPlay = false;
            return;
        }
        if(playerParent.childCount == 1)
        {
            GAME.MANAGER.SwitchTo(State.waiting);
            EVENTS.InvokeVictory();
            isPlay = false;
        }
    }

    private void DespawnPlayer()
    {
        
    }
}
