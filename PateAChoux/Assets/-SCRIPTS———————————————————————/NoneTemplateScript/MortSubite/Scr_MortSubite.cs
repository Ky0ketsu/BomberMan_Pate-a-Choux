using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using Unity.VisualScripting;

public class Scr_MortSubite : MonoBehaviour
{
    public float time;
    public float delayBetweenBlock;

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
        currentBlock.transform.position = transform.position;
        if (nextMoveIsTp)
        {
            transform.DOMove(currentCheckpoint.GetComponentInParent<Scr_CheckPoint>().nextCheckPoint.transform.position, delayBetweenBlock);
            nextMoveIsTp = false;
        }
        else transform.DOMove(transform.position + currentDir, delayBetweenBlock);

        if (!isFinish)
        {
            Invoke("DelayBeforeNewBlock", delayBetweenBlock);
        }
        else Debug.Log("Mort Subite Terminer");
        
    }

    private void DelayBeforeNewBlock()
    {
        MortSubiteFonction();
    }


    private bool nextMoveIsTp;
    private GameObject currentCheckpoint;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            currentCheckpoint = other.gameObject;
            isFinish = other.GetComponentInParent<Scr_CheckPoint>().lastMove;
            nextMoveIsTp = other.GetComponentInParent<Scr_CheckPoint>().nextMoveIsTp;
            currentDir = other.GetComponentInParent<Scr_CheckPoint>().dirWanted;
        }
    }

    private bool isFinish;

}
