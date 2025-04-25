using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scr_Bomb_CaramelExplosion : MonoBehaviour
{
    public GameObject[] preset;
    private GameObject currentZone;
    private GameObject waitForDelete;

    public float duration;

    public int currentSize;
    public Transform parent;
    private Transform owner;

    public void PlaceZone(Transform currentOwner)
    {
        owner = currentOwner;
        currentZone = Instantiate(preset[0], parent);
        currentZone.transform.localScale = new Vector3(0,1,0);
        currentZone.transform.position = new Vector3(owner.position.x, 0, owner.position.z);
        currentZone.transform.DOScale(Vector3.one , 1).SetEase(Ease.OutCubic);

        currentSize = 0;
        StartCoroutine(DurationZone());
    }

    public void resize(int size)
    {
        StopAllCoroutines();
        StartCoroutine(DurationZone());

        waitForDelete = currentZone;
        currentZone.transform.DOScale(new Vector3(0,1,0), 1).SetEase(Ease.OutBounce);
        currentZone = Instantiate(preset[currentSize], parent);
        currentZone.transform.position = new Vector3(owner.position.x, 0, owner.position.z);
        currentZone.transform.localScale = new Vector3(0, 1, 0);
        currentZone.transform.DOScale(Vector3.one, 1).SetEase(Ease.InBounce);

        StartCoroutine(WaitToDelete());
    }

    IEnumerator WaitToDelete()
    {
        yield return new WaitForSeconds(1);
        Destroy(waitForDelete);
    }

    public void DeleteZone()
    {
        currentZone.transform.DOMoveY(-10, 1).SetEase(Ease.OutBounce);
    }

    IEnumerator DurationZone()
    {
        yield return new WaitForSeconds(duration);
        currentZone.transform.DOScale(new Vector3(0, 1, 0), 1).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1);
        Destroy(currentZone);
        Destroy(gameObject);
    }
}
