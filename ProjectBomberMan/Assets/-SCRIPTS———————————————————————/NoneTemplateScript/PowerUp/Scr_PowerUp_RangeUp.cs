using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_RangeUp : MonoBehaviour
{
    public GameObject particule;
    private Transform parentParticule;

    private void Start()
    {
        parentParticule = GameObject.Find("ParticuleParent").transform;
    }

    public void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            GameObject currentParticule = Instantiate(particule, parentParticule);
            currentParticule.transform.position = transform.position;

            other.GetComponent<Scr_Player_Bomb>().range++;
            Destroy(gameObject);
        }
    }
}
