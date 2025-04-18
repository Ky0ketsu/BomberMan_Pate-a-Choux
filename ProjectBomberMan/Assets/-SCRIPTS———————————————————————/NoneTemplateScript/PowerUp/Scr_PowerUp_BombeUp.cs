using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_BombeUp : MonoBehaviour
{
    public GameObject particule;
    private Transform parentParticule;

    public void OnTriggerEnter(Collider other)
    {
        parentParticule = GameObject.Find("ParticuleParent").transform;

        if(other.CompareTag("Player"))
        {
            GameObject currentParticule = Instantiate(particule, parentParticule);
            currentParticule.transform.position = transform.position;

            other.GetComponent<Scr_Player_Bomb>().stockBomb++;
            Destroy(gameObject);
        }
    }
}
