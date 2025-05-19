using System.Collections;
using UnityEngine;
using Rewired;
using System.Linq;

public class Scr_ManagerPlayer : MonoBehaviour
{
    [SerializeField] private bool canInput;
    [SerializeField] private bool isPlay;

    public GameObject playerPrefab;
    public GameObject playerSlotMenu;
    private Transform playerParent;

    public Vector3[] playerSpawnPos = new Vector3[4];

    [SerializeField] public int[] win = new int[4];

    public static Scr_ManagerPlayer acces;


    Controller[] activeControllers = new Controller[4];
    GameObject[] activePlayers = new GameObject[4];


    void Awake()
    {
        acces = this;
        EVENTS.OnLobby += EnablePlayerAdd;
        EVENTS.OnLobbyExit += DisablePlayerAdd;
        EVENTS.OnGameStart += Spawn;
        ResetAllControllers();
    }

    void OnDestroy()
    {
        EVENTS.OnLobby -= EnablePlayerAdd;
        EVENTS.OnLobbyExit -= DisablePlayerAdd;
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
        ResetAllControllers();
    }

    void DisablePlayerAdd()
    {
        canInput = false;
    }




    private void Update()
    {
        Controller activeController = ReInput.controllers.GetLastActiveController();

        if (ReInput.players.GetSystemPlayer().controllers.ContainsController(activeController)==false)
        ReInput.players.GetSystemPlayer().controllers.AddController(activeController, false);
        

        if(canInput)
        {
            int nextAvailablePlayer = FirstAvailablePlayer();
            if (nextAvailablePlayer>=0 && ReInput.controllers.GetAnyButtonDown())
            {
                activeController = ReInput.controllers.GetLastActiveController();
                TryJoin(nextAvailablePlayer, activeController);
                return;
            }
            foreach (Player player in ReInput.players.GetPlayers()) if (player.GetButtonDown("Unjoin")) PlayerQuit(player.id);
        }
        if (isPlay) CheckPlayerAlive();
    }

    void TryJoin(int id, Controller myController)
    {
        if (myController.type==ControllerType.Mouse) return;
        Debug.Log("TRY JOIN "+id+" "+myController.name);
        Player myPlayer = ReInput.players.GetPlayer(id);
        if (myPlayer.isPlaying) return;

        myPlayer.controllers.ClearAllControllers();
        bool removeFromOtherPlayers = true;
        if (myController.type==ControllerType.Keyboard)
        {
            if (ReInput.controllers.Keyboard.GetKeyDown(KeyCode.Space) && leftKeyboardAssigned==false)
            {
                leftKeyboardAssigned = true;
                ReInput.players.GetPlayer(id).controllers.maps.SetMapsEnabled(true, ControllerType.Keyboard,0, 1);
            }
            else if (ReInput.controllers.Keyboard.GetKeyDown(KeyCode.Return) && rightKeyboardAssigned==false)
            {
                rightKeyboardAssigned = true;
                ReInput.players.GetPlayer(id).controllers.maps.SetMapsEnabled(true, ControllerType.Keyboard,0, 2);
            }
            else return; // clavier déjà pris on annule
            removeFromOtherPlayers = false;
            AddControllerToPlayer(myPlayer, myController, removeFromOtherPlayers);
        }
        else if (activeControllers.Contains(myController))
        {
            Debug.Log("HAHAHA VOUS N'AVEZ PAS DIT LE MOT MAGIQUE");
            return;
        }
        else
        {
            AddControllerToPlayer(myPlayer, myController, removeFromOtherPlayers);
        }
    }

    void AddControllerToPlayer(Player myPlayer, Controller myController, bool removeFromOtherPlayers)
    {
            myPlayer.controllers.AddController(myController, removeFromOtherPlayers);
            myPlayer.isPlaying = true;
            activeControllers[myPlayer.id]=myController;
            totalPlayers++;
            playerSlotMenu.transform.GetChild(myPlayer.id).GetComponent<Scr_Menu_Lobby_PlayerAnimation>().AnimationSpawn();
            Debug.Log("PLAYER JOIN "+myPlayer.id+" "+myController.name);
    }

    void PlayerQuit(int id)
    {
        return;
        // À CORRIGER

        Player thisPlayer = ReInput.players.GetPlayer(id);
        if (thisPlayer.isPlaying==false) return;
        thisPlayer.isPlaying = false;

        if (thisPlayer.controllers.maps.GetMap(ControllerType.Keyboard,0,0).enabled)
        {
            thisPlayer.controllers.maps.SetMapsEnabled(false, ControllerType.Keyboard, 0, 1);
            leftKeyboardAssigned = false;
        }
        if (thisPlayer.controllers.maps.GetMap(ControllerType.Keyboard,0,1).enabled)
        {
            thisPlayer.controllers.maps.SetMapsEnabled(false, ControllerType.Keyboard, 0, 2);
            rightKeyboardAssigned = false;
        }
        activeControllers[id]=null;
        thisPlayer.controllers.ClearAllControllers();
        playerSlotMenu.transform.GetChild(id).GetComponent<Scr_Menu_Lobby_PlayerAnimation>().AnimationDespawn();
        totalPlayers--;
    }




    private void Spawn()
    {
        playerParent = GameObject.Find("ActivePlayerParent").transform;
        for (int i = 0; i < 4; i++)
        {
            if (ReInput.players.GetPlayer(i).isPlaying == true)
            {
                GameObject currentPlayer = Instantiate(playerPrefab, playerSpawnPos[i],transform.rotation, playerParent);
                activePlayers[i] = currentPlayer;
                currentPlayer.GetComponent<PlayerMove>().playerID = i;
                currentPlayer.name = "Joueur " + (i+1);
            }
        }
        isPlay = true;

        StartCoroutine(DelayToGameplay());
    }

    void CheckPlayerAlive()
    {
        if(!isPlay) return;

        if(playerParent.childCount <= 1)
        {
            isPlay = false;
            StartCoroutine(DelayToRecheck());
        }
    }

    IEnumerator DelayToRecheck()
    {
        yield return new WaitForSeconds(0.1f);
        if (playerParent.childCount == 0)
        {
            GAME.MANAGER.SwitchTo(State.waiting);
            EVENTS.InvokeDefeat();
        }
        if (playerParent.childCount == 1)
        {
            GAME.MANAGER.SwitchTo(State.waiting);
            EVENTS.InvokeVictory();
            AddScore(playerParent.GetChild(0).GetComponent<PlayerMove>().playerID);
        }
    }

    private void AddScore(int ID)
    {
        win[ID]++;
    }


//--------------------------------------------------------------------------------------------------------------------
// karl

public int totalPlayers = 0;
bool rightKeyboardAssigned, leftKeyboardAssigned = false;

    void ResetAllControllers()
    {
        totalPlayers = 0;
        activeControllers = new Controller[4];
        foreach (Player player in ReInput.players.GetPlayers())
        {
            player.controllers.maps.SetMapsEnabled(false, ControllerType.Keyboard, 0, 1);
            player.controllers.maps.SetMapsEnabled(false, ControllerType.Keyboard, 0, 2);
            player.controllers.ClearAllControllers();
            player.isPlaying = false;
        }
        Player systemPlayer = ReInput.players.GetSystemPlayer();
        systemPlayer.controllers.ClearAllControllers();
        systemPlayer.controllers.AddController(ControllerType.Mouse, 0, true);
        systemPlayer.controllers.AddController(ControllerType.Keyboard,0,false);
        leftKeyboardAssigned = rightKeyboardAssigned = false;
        //Debug.Log("Reset all controllers   SystemPlayer " + ReInput.players.GetSystemPlayer().controllers);
    }


    int FirstAvailablePlayer()
    {
        for (int i=0;i<4;i++)
        {
            if (ReInput.players.GetPlayer(i).isPlaying==false) return i;
        }
        return -1;
    }

    [SerializeField] float timer;

    IEnumerator DelayToGameplay()
    {
        float chrono = timer;
        while (chrono > 0)
        {
            chrono -= Time.deltaTime;
            yield return null;
        }

        Debug.Log("Can move");
        GAME.MANAGER.SwitchTo(State.gameplay);
    }


} // FIN DU SCRIPT
