using DG.Tweening;
using Rewired;
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
    private bool canAim;
    public Vector3 offsett;

    private Vector3 currentZonePosition;

    private Player player;

    private void Start()
    {
        StartAim();
    }

    public void StartAim()
    {
        owner.GetComponent<PlayerMove>().CanRun = false;
        currentVisualizeZone = Instantiate(visualizeZonePrefab, transform);
        currentZonePosition = transform.position;
        canAim = true;
        canShoot = false;
        player = ReInput.players.GetPlayer(owner.GetComponent<PlayerMove>().playerID);
        StartCoroutine(DelayBeforeCanShoot());
    }
    private bool canShoot;


    private void Update()
    {
        if(canAim)
        {
            if (player.GetAxis("MoveVertical") < 0) currentZonePosition = currentZonePosition + Vector3.back * 2; DelayBetweenAim(); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            if (player.GetAxis("MoveVertical") > 0) currentZonePosition = currentZonePosition + Vector3.forward * 2; DelayBetweenAim(); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            if (player.GetAxis("MoveHorizontal") > 0) currentZonePosition = currentZonePosition + Vector3.right * 2; DelayBetweenAim(); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            if (player.GetAxis("MoveHorizontal") < 0) currentZonePosition = currentZonePosition + Vector3.left * 2; DelayBetweenAim(); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
        }

        if(player.GetButtonDown("Bomb") && canShoot)
        {
            Debug.Log("Blitz");
        }
    }

    IEnumerator DelayBetweenAim()
    {
        yield return new WaitForSeconds(0.1f);
        canAim = true;
    }
    
    IEnumerator DelayBeforeCanShoot()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    

    public void Fire()
    {
        currentExplosionZone = Instantiate(ExplosionZonePrefab, transform);
        currentExplosionZone.transform.position = currentZonePosition;
    }
}
