using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Bomb_Propagation : MonoBehaviour
{
    public int range;

    public LayerMask mask;

    private bool canLeft, canRight, canForward, canBack;

    public void Explosion()
    {
        for (int i = 0; i < range; i++)
        {
            if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hitLeft, 2, mask))
            {

            }

            if(Physics.Raycast(transform.position, Vector3.right, out RaycastHit hitRight, 2, mask))
            {

            }

            if(Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitForward, 2, mask))
            {

            }

            if(Physics.Raycast(transform.position, Vector3.back, out RaycastHit hitBack, 2, mask))
            {

            }


        }
    }

}
