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
        if(other.GetComponentInParent<PlayerMove>())
        {
            GameObject currentParticule = Instantiate(particulePrefab, particuleParent);
            currentParticule.transform.position = transform.position;

            other.GetComponentInParent<Scr_Player_Bomb>().blitzActive = true;

            Delete();
        }
    }

    private void Delete()
    {
        Destroy(gameObject);
    }
}
