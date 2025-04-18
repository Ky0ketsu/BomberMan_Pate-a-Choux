using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Scr_MortSubite : MonoBehaviour
{
    public float time;
    public float delayBetweenBlock;

    public GameObject spawner;
    public GameObject blockPrefab;
    public Transform parent;

    public LayerMask mask;

    private Vector3 currentDir;

    void Start()
    {
        spawner.SetActive(false);
    }

    void OnEnable()
    {  
    EVENTS.OnGameStart += StartTimer;
    }
    void OnDisable()
    {
    EVENTS.OnGameStart -= StartTimer;
    }

    public void StartTimer()
    {
        StartCoroutine(StartMortSubite());
    }

    IEnumerator StartMortSubite()
    {
        yield return new WaitForSeconds(time);
        spawner.SetActive(true);
        MortSubiteFonction();
    }

    private void MortSubiteFonction()
    {
        GameObject currentBlock = Instantiate(blockPrefab, parent);
        transform.DOMove(transform.position + currentDir, delayBetweenBlock);
        StartCoroutine(DelayBetweenBlock());
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            currentDir = other.GetComponent<Scr_CheckpointDir>().dir;
        }
    }

    IEnumerator DelayBetweenBlock()
    {
        yield return new WaitForSeconds(delayBetweenBlock);
    }

}
