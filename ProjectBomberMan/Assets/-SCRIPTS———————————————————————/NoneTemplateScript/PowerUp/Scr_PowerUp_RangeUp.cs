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
            other.GetComponentInParent<Scr_Player_Bomb>().range++;
            Take();
        }

        if(other.GetComponentInParent<Scr_Bomb_Frozen>() != null)
        {
            other.GetComponentInParent<Scr_Bomb_Propagation>().range++;
            Take();
        }
    }

    private void Take()
    {
        GameObject currentParticule = Instantiate(particule, parentParticule);
        currentParticule.transform.position = transform.position;

        Destroy(gameObject);
    }
}
