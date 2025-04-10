using Rewired.ControllerExtensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_PowerUp_Frozen : MonoBehaviour
{
    public GameObject particule;
    private Transform particuleParent;

    public float timeEffect;

    private GameObject currentPlayer;

    public void OnTriggerEnter(Collider other)
    {
        particuleParent = GameObject.Find("ParticulParent").transform;

        if(other.CompareTag("Player"))
        {
            GameObject currentParticule = Instantiate(particule, particuleParent);
            currentParticule.transform.position = transform.position;

            currentPlayer = other.gameObject;
            other.GetComponent<Scr_Player_Bomb>().frozenActive = true;
            StartCoroutine(TimeEffect());
        }
    }

    IEnumerator TimeEffect()
    {
        yield return new WaitForSeconds(timeEffect);
        currentPlayer.GetComponent<Scr_Player_Bomb>().frozenActive = false;
    }
}
