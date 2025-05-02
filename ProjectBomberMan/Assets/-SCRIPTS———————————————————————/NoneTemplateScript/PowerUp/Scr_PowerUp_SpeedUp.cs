using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_SpeedUp : MonoBehaviour
{
    public GameObject particule;
    private Transform parentParticule;

    public void OnTriggerEnter(Collider other)
    {
        parentParticule = GameObject.Find("ParticuleParent").transform;

        var player = other.GetComponentInParent<PlayerMove>();
        if (player)
        {
            GameObject currentParticule = Instantiate(particule, parentParticule);
            currentParticule.transform.position = transform.position;

            player.startMaxSpeed *= 1.2f;
            player.maxSpeed = player.startMaxSpeed;
            Destroy(gameObject);
        }
    }
}
