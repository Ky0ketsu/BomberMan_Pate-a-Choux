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

    private Vector3 currentDir;

    void Start()
    {
        currentDir = new Vector3(2,0,0);
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
        MortSubiteFonction();
    }

    private void MortSubiteFonction()
    {
        GameObject currentBlock = Instantiate(blockPrefab, parent);
        currentBlock.transform.position = spawner.transform.position;
        if (nextMoveIsTp)
        {
            transform.DOMove(currentCheckpoint.GetComponentInParent<Scr_CheckpointDir>().nextPos.position, delayBetweenBlock);
            nextMoveIsTp = false;
        }
        else transform.DOMove(transform.position + currentDir, delayBetweenBlock);

        if (!isFinish)
        {
            StartCoroutine(DelayBetweenBlock());
        }
        else Debug.Log("Mort Subite Terminer");
        
    }

    private bool nextMoveIsTp;
    private GameObject currentCheckpoint;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            currentCheckpoint = other.gameObject;
            if(other.GetComponentInParent<Scr_CheckpointDir>().lastCheckpoint == true)
            {
                isFinish = true;
            }
            nextMoveIsTp = other.GetComponentInParent<Scr_CheckpointDir>().typeTp;
            currentDir = other.GetComponentInParent<Scr_CheckpointDir>().dir;
        }
    }

    private bool isFinish;

    IEnumerator DelayBetweenBlock()
    {
        yield return new WaitForSeconds(delayBetweenBlock);
        MortSubiteFonction();
        
    }

}
