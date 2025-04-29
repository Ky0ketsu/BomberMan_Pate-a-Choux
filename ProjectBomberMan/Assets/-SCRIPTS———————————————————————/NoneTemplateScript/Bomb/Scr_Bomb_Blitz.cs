using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Bomb_Blitz : MonoBehaviour
{
    public GameObject owner;

    public GameObject visualizeZonePrefab;
    public GameObject ExplosionZonePrefab;

    private GameObject currentVisualizeZone;
    private GameObject currentExplosionZone;
    private bool isShooting;
    public Vector3 offsett;

    private Vector3 currentZonePosition;

    private void Start()
    {
        owner = GetComponent<Scr_Bomb>().owner;
        StartShoot();
    }

    public void StartShoot()
    {
        owner.GetComponent<PlayerMove>().CanRun = false;
        currentVisualizeZone = Instantiate(visualizeZonePrefab, transform);
        isShooting = true;
    }

    private void Update()
    {
        if(isShooting)
        {
            if (Input.GetButtonDown("Back")) currentZonePosition = currentZonePosition + Vector3.back * 2;
            if (Input.GetButtonDown("Forward")) currentZonePosition = currentZonePosition + Vector3.forward * 2;
            if (Input.GetButtonDown("Right")) currentZonePosition = currentZonePosition + Vector3.right * 2;
            if (Input.GetButtonDown("Left")) currentZonePosition = currentZonePosition + Vector3.left * 2;

            currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);

            //if (Input.GetButtonDown("Bomb")) Fire();
        }
    }

    

    public void Fire()
    {
        currentExplosionZone = Instantiate(ExplosionZonePrefab, transform);
        currentExplosionZone.transform.position = currentZonePosition;
    }
}
