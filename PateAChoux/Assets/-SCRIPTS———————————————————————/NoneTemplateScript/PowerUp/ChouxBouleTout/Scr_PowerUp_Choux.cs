using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_Choux : MonoBehaviour
{
    [SerializeField] private float timeEffect;

    private bool effectUse = false;
    [SerializeField] private GameObject TriggerPrefab;
    private GameObject currentCol;


    private void Update()
    {
        if (!effectUse && GetComponent<Scr_PowerUp_Default>().player != null)
        {
            currentCol = Instantiate(TriggerPrefab, GetComponent<Scr_PowerUp_Default>().player.transform.position, transform.rotation, GetComponent<Scr_PowerUp_Default>().player.transform);
            effectUse = true;
            StartCoroutine(TimeEffect());
        }
    }

    IEnumerator TimeEffect()
    {
        yield return new WaitForSeconds(timeEffect);
        Destroy(currentCol);
        Destroy(gameObject);
    }
}
