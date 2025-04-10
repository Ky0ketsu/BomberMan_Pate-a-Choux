using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;

public class Scr_Terrain_Generator : MonoBehaviour
{
    public GameObject[] PresetList;
    public Vector3 offset;
    public Transform terrainParent;


    public void Start()
    {
        EVENTS.OnGameStart += GenerateTerrain;
    }

    public void GenerateTerrain()
    {
        if (terrainParent == null)
        {
            GameObject currentTerrain = Instantiate(PresetList[Random.Range(0, PresetList.Length)]);
            currentTerrain.transform.position = Vector3.zero + offset;
        }
        
    }
}
