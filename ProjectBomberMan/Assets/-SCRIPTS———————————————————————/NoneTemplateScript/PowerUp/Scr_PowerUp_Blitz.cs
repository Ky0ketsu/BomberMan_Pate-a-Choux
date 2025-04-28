using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_Blitz : MonoBehaviour
{
    public GameObject particulePrefab;
    public Transform particuleParent;

    private void Start()
    {
        particuleParent = GameObject.Find("ParticuleParent").transform;   
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject currentParticule = Instantiate(particulePrefab, particuleParent);
            currentParticule.transform.position = transform.position;

            other.GetComponentInParent<Scr_Player_Bomb>().nextShootIsSpecial = true;
            other.GetComponentInParent<Scr_Player_Bomb>().blitzType = true;

            Delete();
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
