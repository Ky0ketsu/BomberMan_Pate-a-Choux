using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CheckPoint : MonoBehaviour
{
    public Vector2[] dir;
    public Vector2 dirWanted;
    public bool nextMoveIsTp;
    public bool lastMove;
    public GameObject nextCheckPoint;

    public void SetInfo(int ID, int dirInt, int type, GameObject nextCheckpoint)
    {
        dirWanted = dir[dirInt];
        if(type == 1) nextMoveIsTp = true;
        if(type == 2) lastMove = true;
    }
}
