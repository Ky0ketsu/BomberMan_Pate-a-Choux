using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Player_Bomb : MonoBehaviour
{
    public GameObject bombPrefab, bombFrozenPrefab, bombCaramelPrefab, bombBlitzPrefab;
    private GameObject CurrentBomb;
    public Transform bombParent;
    public LayerMask mask;
    public int range;

    public Vector3 offset;

    public int stockBomb;

    public bool frozenActive;
    public bool caramelActive;
    private GameObject currentBomb;

    public bool nextShootIsSpecial;
    public bool blitzType;

    private bool canBomb;

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
            if (Input.GetButtonDown("Bomb"))
            {
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), new Vector3(0, -1, 0), out RaycastHit hit, Mathf.Infinity, mask))
                {
                    Transform currentCase = hit.transform.GetChild(0);
                    Debug.Log(currentCase);

                    if(!nextShootIsSpecial)
                    {
                        if (currentCase.childCount <= 0 && stockBomb > 0)
                        {
                            if (frozenActive)
                            {
                                currentBomb = Instantiate(bombFrozenPrefab, currentCase);
                            }else
                            if (caramelActive)
                            {
                                currentBomb = Instantiate(bombCaramelPrefab, currentCase);
                            }
                            else
                            {
                                currentBomb = Instantiate(bombPrefab, currentCase);
                            }

                            
                        }
                    }else
                    {
                        if(blitzType)
                        {
                            currentBomb = Instantiate(bombBlitzPrefab, currentCase);
                        }
                    }

                    currentBomb.transform.position = currentCase.position + offset;
                    stockBomb--;
                    currentBomb.GetComponent<Scr_Bomb>().owner = gameObject;
                    if(currentBomb.GetComponent<Scr_Bomb_Propagation>() != null) currentBomb.GetComponent<Scr_Bomb_Propagation>().range = range;
                }
            }
        }
    }
    
    

}
