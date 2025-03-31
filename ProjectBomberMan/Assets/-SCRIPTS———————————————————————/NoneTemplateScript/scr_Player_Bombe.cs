using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Player_Bombe : MonoBehaviour
{
    public GameObject bombPrefab;
    private GameObject CurrentBomb;
    public Transform bombParent;
    //public LayerMask layerMask;

    public float speed;

    private void Update()
    {

        if (Input.GetButtonDown("Bombe"))
        {
            GameObject currentBomb = Instantiate(bombPrefab, bombParent);
        }
        //SnapingBomb();
    }
    
    /*private void SnapingBomb()
    {
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out RaycastHit hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * hit.distance, Color.green);
            Debug.Log("touche");
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 100f, Color.red);
            Debug.Log("Touche pas");
        }
    }*/

}
