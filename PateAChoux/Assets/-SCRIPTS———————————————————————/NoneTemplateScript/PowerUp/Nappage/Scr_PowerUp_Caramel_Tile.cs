using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PowerUp_Caramel_Tile : MonoBehaviour
{
    public Vector3 maxPos;

    private void LateUpdate()
    {
        if (transform.position.x < -maxPos.x || transform.position.x > maxPos.x || transform.position.z > maxPos.z || transform.position.z < -maxPos.z)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<PlayerMove>() != null) other.GetComponentInParent<PlayerMove>().SetSpeed(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.GetComponentInParent<PlayerMove>() != null) other.GetComponentInParent<PlayerMove>().SetSpeed(false);
    }
}
