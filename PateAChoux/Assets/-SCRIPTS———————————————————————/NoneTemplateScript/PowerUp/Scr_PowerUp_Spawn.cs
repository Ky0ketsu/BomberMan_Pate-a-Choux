using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_Spawn : MonoBehaviour
{
    public Vector3 offset;
    public GameObject[] powerUp;

    [SerializeField] int probability;
    
    
    public Transform powerUpParent;

    public void SpawnPowerUp()
    {
        int prob = Random.Range(0, 100);
        if (prob <= probability)
        {
            GameObject currentPowerUp = Instantiate(powerUp[Random.Range(0, powerUp.Length)], powerUpParent);
            currentPowerUp.transform.position = transform.position + offset;
        }   
    }
}
