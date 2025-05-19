using DG.Tweening;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Scr_Bomb_Blitz : MonoBehaviour
{
    public GameObject owner;

    public GameObject visualizeZonePrefab;
    public GameObject explosionZonePrefab;

    private GameObject currentVisualizeZone;
    private GameObject currentExplosionZone;
    private bool canAim;
    public Vector3 offsett;

    private Vector3 currentZonePosition;

    private Player player;
    private bool canShoot;

    [SerializeField] private GameObject[] particule = new GameObject[3];
    [SerializeField] private GameObject missile;

    public void StartAim()
    {
        owner.GetComponent<PlayerMove>().CanRun = false;
        currentVisualizeZone = Instantiate(visualizeZonePrefab, transform.position, transform.rotation, transform);
        canAim = true;
        canShoot = false;
        player = ReInput.players.GetPlayer(owner.GetComponent<PlayerMove>().playerID);
        StartCoroutine(DelayBeforeCanShoot());
    }
    


    private void Update()
    {
        if(canAim)
        {

            float currentDirX = player.GetAxis("MoveHorizontal");
            float currentDirY = player.GetAxis("MoveVertical");


            if (currentDirX < 0) currentDirX = -currentDirX;
            if (currentDirY < 0) currentDirY = -currentDirY;

            if (currentDirX < currentDirY)
            {
                if (player.GetAxis("MoveVertical") < 0)
                {
                    currentZonePosition = currentZonePosition + Vector3.back * 2;
                    StartCoroutine(DelayBetweenAim());
                    canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
                }
                else currentZonePosition = currentZonePosition + Vector3.forward * 2; StartCoroutine(DelayBetweenAim()); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            }
            if (currentDirY < currentDirX)
            {
                if (player.GetAxis("MoveHorizontal") < 0)
                {
                    currentZonePosition = currentZonePosition + Vector3.left * 2;
                    StartCoroutine(DelayBetweenAim()); canAim = false;
                    currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
                }
                else currentZonePosition = currentZonePosition + Vector3.right * 2; StartCoroutine(DelayBetweenAim()); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            }



            /*if (player.GetAxis("MoveVertical") < 0) currentZonePosition = currentZonePosition + Vector3.back * 2; StartCoroutine(DelayBetweenAim()); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            if (player.GetAxis("MoveVertical") > 0) currentZonePosition = currentZonePosition + Vector3.forward * 2; StartCoroutine(DelayBetweenAim()); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            if (player.GetAxis("MoveHorizontal") > 0) currentZonePosition = currentZonePosition + Vector3.right * 2; StartCoroutine(DelayBetweenAim()); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            if (player.GetAxis("MoveHorizontal") < 0) currentZonePosition = currentZonePosition + Vector3.left * 2; StartCoroutine(DelayBetweenAim()); canAim = false; currentVisualizeZone.transform.DOMove(currentZonePosition + offsett, 0.1f).SetEase(Ease.InOutCubic);
            */
        }

        if(player.GetButtonDown("Bomb") && canShoot)
        {
            ShootStep1();
        }
    }

    IEnumerator DelayBetweenAim()
    {
        yield return new WaitForSeconds(0.2f);
        canAim = true;
    }
    
    IEnumerator DelayBeforeCanShoot()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    [HideInInspector] private GameObject currentMissile;

    private void ShootStep1()
    {
        currentMissile = Instantiate(missile, transform.position, transform.rotation, transform);
        GameObject currentParticule = Instantiate(particule[0], transform.position, transform.rotation, currentMissile.transform);
        currentMissile.transform.DOMoveY(30, 2f).SetEase(Ease.OutCubic).OnComplete(ShootStep2);

        Invoke("ShootStep2", 3f);
    }

    private void ShootStep2()
    {
        currentMissile.transform.position = new Vector3(currentZonePosition.x, currentMissile.transform.position.z, currentZonePosition.z);
        currentMissile.transform.DOMoveY(0, 3f).SetEase(Ease.InCubic).OnComplete(ShootStep3);
    }

    private void ShootStep3()
    {
        currentVisualizeZone = Instantiate(explosionZonePrefab, transform.position, transform.rotation, transform);
    }
}
