using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Player_Bomb : MonoBehaviour
{
    public GameObject bombPrefab, bombFrozenPrefab, bombCaramelPrefab, bombBlitzPrefab;
    private GameObject currentBomb;
    private Transform currentCase;
    public Transform bombParent;
    public LayerMask mask;
    public int range;

    public Vector3 offset;

    public int stockBomb;

    public bool frozenActive, caramelActive, blitzActive;

    [HideInInspector]public bool canBomb;

    [SerializeField]private int playerID;

    public void Awake()
    {
        EVENTS.OnGameplay += EnableBomb;
        EVENTS.OnGameplayExit += DisableBomb;
    }

    public void OnDestroy()
    {
        EVENTS.OnGameplay -= EnableBomb;
        EVENTS.OnGameplayExit += DisableBomb;
    }

    void EnableBomb()
    {
        canBomb = true;
    }

    void DisableBomb()
    {
        canBomb = false;
    }

    private void Update()
    {
        if(canBomb)
        {
            playerID = transform.GetComponent<PlayerMove>().playerID;
            if (ReInput.players.GetPlayer(playerID).GetButtonDown("Bomb") && stockBomb > 0)
            {
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), new Vector3(0, -1, 0), out RaycastHit hit, Mathf.Infinity, mask))
                {
                    currentCase = hit.transform.GetComponentInParent<Dalle>().slotBomb;
                    Debug.Log(currentCase);

                    if (currentCase.childCount <= 0)
                    {
                        if (frozenActive)
                        {
                            Debug.Log("FrozenActive");
                            CreateBomb(bombFrozenPrefab);
                            frozenActive = false; // TODO : Rectif du frozenActive

                        }
                        else if (caramelActive)
                        {
                            CreateBomb(bombCaramelPrefab);
                            caramelActive = false; // TODO : Si bug avec blitz suite à ma modif, supprimer cette ligne.
                        }
                        else if (blitzActive)
                        {
                            CreateBomb(bombBlitzPrefab);
                            canBomb = false;
                            blitzActive = false; // TODO : Si bug avec blitz suite à ma modif, supprimer cette ligne.
                        }
                        else CreateBomb(bombPrefab);
                    }
                }
            }
        }
    }
    
    private void CreateBomb(GameObject bomb)
    {
        currentBomb = Instantiate (bomb, currentCase);

        currentBomb.transform.position = currentCase.position + offset;
        stockBomb--;
        if(currentBomb.GetComponent<Scr_Bomb>() != null) currentBomb.GetComponent<Scr_Bomb>().owner = gameObject;
        if(currentBomb.GetComponent<Scr_Bomb_Propagation>() != null) currentBomb.GetComponent<Scr_Bomb_Propagation>().range = range;
        if(currentBomb.GetComponent<Scr_Bomb_Blitz>() != null) currentBomb.GetComponent<Scr_Bomb_Blitz>().owner = gameObject;
        if(currentBomb.GetComponent<Scr_Bomb_Blitz>() != null) currentBomb.GetComponent<Scr_Bomb_Blitz>().StartAim();
        currentBomb = null;
    }

}
