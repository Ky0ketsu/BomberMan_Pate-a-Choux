using UnityEngine;

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
        if (terrainParent != null)
        {
            GameObject currentTerrain = Instantiate(PresetList[Random.Range(0, PresetList.Length)],terrainParent);
            currentTerrain.transform.position = Vector3.zero + offset;
        }
        else
        {
            Debug.Log("Pas de Terrain");
        }
        
    }
}
