using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_Spawn : MonoBehaviour
{
    [SerializeField]private Vector3 offset;
    public GameObject[] powerUp;

    [SerializeField] int probability;
    
    
    public Transform powerUpParent;

    public void SpawnPowerUp()
    {
        powerUpParent = GameObject.Find("PowerUpParent").transform;

        int prob = Random.Range(0, 100);
        if (prob <= probability)
        {
            Instantiate(powerUp[Random.Range(0, powerUp.Length)],transform.position + offset,transform.rotation, powerUpParent);
        }   
    }
}
