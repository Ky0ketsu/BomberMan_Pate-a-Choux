using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_Default : MonoBehaviour
{
    public GameObject particule;
    private Transform parentParticule;
    [HideInInspector] public GameObject player, frozen, blitz = null;
    [SerializeField] float delay;
    [SerializeField] GameObject graphics, colliders;

    [SerializeField] bool canGetByFrozen, canGetByBlitz;

    public void OnTriggerEnter(Collider other)
    {
        parentParticule = GameObject.Find("ParticuleParent").transform;

        if(other.GetComponentInParent<PlayerMove>())
        {
            player = other.GetComponentInParent<PlayerMove>().gameObject;
            isGet();
        }

        if(other.GetComponentInParent<Scr_Bomb_Frozen>() && canGetByFrozen)
        {
            frozen = other.GetComponentInParent<Scr_Bomb_Frozen>().gameObject;
            isGet();
        }

        if(other.GetComponentInParent<Scr_Bomb_Blitz>() && canGetByBlitz)
        {
            blitz = other.GetComponentInParent<Scr_Bomb_Blitz>().gameObject;
            isGet();
        }
    }

    private void isGet()
    {
        graphics.SetActive(false); colliders.SetActive(false);
        StartCoroutine(DelayToDestroy());

        GameObject currentParticule = Instantiate(particule, parentParticule);
        currentParticule.transform.position = transform.position;
    }
    

    IEnumerator DelayToDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
