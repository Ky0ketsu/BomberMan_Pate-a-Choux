using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CheckPoint : MonoBehaviour
{
    public Vector3[] dir;
    public Vector3 dirWanted;
    public bool nextMoveIsTp;
    public bool lastMove;
    public GameObject col;

    public GameObject nextCheckPoint;

    private void Awake()
    {
        col.SetActive(false);
    }

    public void SetDir(int dirInt, int type)
    {
        dirWanted = dir[dirInt];
        if(type == 1) nextMoveIsTp = true;
        if(type == 2) lastMove = true;
    }
    public void SetNextCheckPoint(GameObject checkpoint)
    {
        nextCheckPoint = checkpoint;
    }

    public void ActiveCol()
    {
        col.SetActive(true);
    }
}
