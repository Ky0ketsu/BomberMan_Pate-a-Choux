using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Player_Bomb : MonoBehaviour
{
    public GameObject bombPrefab;
    private GameObject CurrentBomb;
    public Transform bombParent;
    public LayerMask mask;
    public int range;

    public Vector3 offset;

    public int stockBomb;

    public bool frozenActive;

    private void Update()
    {
        if (Input.GetButtonDown("Bomb"))
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), new Vector3(0, -1, 0), out RaycastHit hit, Mathf.Infinity, mask))
            {
                Transform currentCase = hit.transform.GetChild(0);
                Debug.Log(currentCase);

                if (currentCase.childCount <= 0 && stockBomb > 0)
                {
                    GameObject currentBomb = Instantiate(bombPrefab, currentCase);
                    currentBomb.transform.position = currentCase.position + offset;
                    stockBomb--;
                    currentBomb.GetComponent<Scr_Bomb>().owner = gameObject;
                    currentBomb.GetComponent<Scr_Bomb_Propagation>().range = range;
                    currentBomb.GetComponent<Scr_Bomb_FrozenActive>();
                }
            }
        }
    }
    
    

}
