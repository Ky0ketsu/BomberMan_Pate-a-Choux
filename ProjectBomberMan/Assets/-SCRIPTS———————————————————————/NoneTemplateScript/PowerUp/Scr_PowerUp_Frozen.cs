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
    public GameObject graphics, colliders;

    private void Start()
    {
        particuleParent = GameObject.Find("ParticuleParent").transform;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject currentParticule = Instantiate(particule, particuleParent);
            currentParticule.transform.position = transform.position;

            currentPlayer = other.gameObject;
            graphics.SetActive(false); colliders.SetActive(false);

            other.GetComponentInParent<Scr_Player_Bomb>().frozenActive = true;
            StartCoroutine(TimeEffect());
        }
    }

    IEnumerator TimeEffect()
    {
        yield return new WaitForSeconds(timeEffect);
        currentPlayer.GetComponentInParent<Scr_Player_Bomb>().frozenActive = false;
        Destroy(gameObject);
    }
}
