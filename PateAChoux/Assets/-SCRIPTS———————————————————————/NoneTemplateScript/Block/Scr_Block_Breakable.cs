using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Block_Breakable : MonoBehaviour
{
    public bool canDestroy = true;
    public GameObject particuleDestroy;
    public Transform particulParent;
    public void Explode()
    {
        particulParent = GameObject.Find("ParticuleParent").transform;
        GameObject particule = Instantiate(particuleDestroy,transform.position ,transform.rotation,particulParent);

        gameObject.GetComponent<Scr_PowerUp_Spawn>().SpawnPowerUp();
        Destroy(gameObject);
    }
}
