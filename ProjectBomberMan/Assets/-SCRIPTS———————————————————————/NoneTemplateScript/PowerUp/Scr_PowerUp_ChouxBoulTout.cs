using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_ChouxBoulTout : MonoBehaviour
{
    public GameObject particule;
    private Transform parentParticule;

    public float timeEffect;
    private GameObject player;

    public void OnTriggerEnter(Collider other)
    {
        parentParticule = GameObject.Find("ParticuleParent").transform;

        if (other.GetComponentInParent<PlayerMove>())
        {
            GameObject currentParticule = Instantiate(particule, parentParticule);
            currentParticule.transform.position = transform.position;

            other.GetComponentInParent<PlayerMove>().pushActive = true;
            player = other.gameObject;
            StartCoroutine(TimeEffect());
        }
    }

    IEnumerator TimeEffect()
    {
        yield return new WaitForSeconds(timeEffect);
        player.GetComponentInParent<PlayerMove>().pushActive = false;
        Destroy(gameObject);
    }
}
