using DG.Tweening;
using Rewired;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Bomb : MonoBehaviour
{
    public GameObject owner;
    public float timer;
    public float maxY, minY;

    public GameObject caramelPrefab;

    public Transform graphics;

    [SerializeField] private GameObject particule;

    [SerializeField] private GameObject colliderInternal;


    public void Start()
    {
        StartCoroutine(Timer());
        graphics.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f).SetLoops(-1, LoopType.Yoyo);
        transform.DOMoveY(maxY, 0.5f).SetEase(Ease.OutExpo);
        transform.DOMoveY(minY, 1).SetEase(Ease.InExpo).OnComplete(ParticuleEndAnimation);

        gameObject.layer = 20 + owner.GetComponent<PlayerMove>().playerID;
        colliderInternal.layer = 20 + owner.GetComponent<PlayerMove>().playerID;

        /*foreach (Transform t in gameObject.GetComponentInChildren<Transform>(true))
        {
            if (t.childCount > 0)
            {
                foreach (Transform r in t.gameObject.GetComponentInChildren<Transform>(true))
                {
                    t.gameObject.layer = 20 + owner.GetComponent<PlayerMove>().playerID;
                }
            }
            t.gameObject.layer = 20 + owner.GetComponent<PlayerMove>().playerID;
        }*/
    }

    private void ParticuleEndAnimation()
    {
        Instantiate(particule, transform.position, transform.rotation, GameObject.Find("ParticuleParent").transform);
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(timer);
        Explosion();
    }

    public void Explosion()
    {
        if(owner != null)
        {
            owner.GetComponent<Scr_Player_Bomb>().stockBomb++;
        }

        gameObject.GetComponent<Scr_Bomb_Propagation>().Explosion();

        if(transform.GetComponent<Scr_Bomb_Caramel>())
        {
            GameObject currentZone = Instantiate(caramelPrefab, GameObject.Find("TerrainParent").transform);
            currentZone.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            currentZone.GetComponent<Scr_Bomb_CaramelExplosion>().PlaceZone(transform);
        }

        StartCoroutine(DelayToDestroy());
    }

    IEnumerator DelayToDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


    private void Update()
    {
        if (Vector3.Distance(new Vector3(owner.transform.position.x, 0, owner.transform.position.z), new Vector3(transform.position.x, 0, transform.position.z)) >= 2)
        {
            gameObject.layer = 15;
        }
    }
}
