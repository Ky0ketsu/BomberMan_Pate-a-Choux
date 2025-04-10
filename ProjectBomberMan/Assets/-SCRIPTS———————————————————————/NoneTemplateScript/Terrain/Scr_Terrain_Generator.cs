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


    void OnEnable()
    {
        EVENTS.OnGameStart += GenerateTerrain;
    }
    void OnDisable()
    {
        EVENTS.OnGameStart -= GenerateTerrain;
    }

    public void GenerateTerrain()
    {
        Debug.Log("Terrain");
        if (terrainParent == null)
        {
            GameObject currentTerrain = Instantiate(PresetList[Random.Range(0, PresetList.Length)],terrainParent);
            currentTerrain.transform.position = Vector3.zero + offset;
        }
        
    }
}
