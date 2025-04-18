
using UnityEngine;


public class Scr_Bomb_FrozenActive : MonoBehaviour
{
    public float distance;   

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            /*if(other.transform.position.x > transform.position.x)
            {
                if(other.transform.position.z > transform.position.z)
                {
                    transform.DOMove(transform.position + Vector3.)
                }else
                {

                }
            }else
            {
                if(other.transform.position.z > transform.position.z)
                {

                }else
                {

                }
            }*/
        }
    }
}
