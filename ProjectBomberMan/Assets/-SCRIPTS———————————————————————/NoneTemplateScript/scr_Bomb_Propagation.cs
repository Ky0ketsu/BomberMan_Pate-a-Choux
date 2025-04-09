using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scr_Bomb_Propagation : MonoBehaviour
{
    public int range;

    public LayerMask mask;

    private bool canLeft, canRight, canForward, canBack;

    public GameObject propagation;
    public Transform propParent;

    private void Start()
    {
        canBack = canForward = canLeft = canRight = true; 
    }

    public void Explosion()
    {
        for (int i = 1; i < range+1; i++)
        {
            if(canLeft)
            {
                if (Physics.Raycast(transform.position, Vector3.left, out RaycastHit hitLeft,i * 2, mask))
                {
                    canLeft = false;
                    GameObject currenthitLeft = hitLeft.transform.parent.parent.gameObject;

                    if (currenthitLeft.GetComponent<Scr_Block_Breakable>() != null)
                    {
                        currenthitLeft.GetComponent<Scr_Block_Breakable>().Explode();
                        GameObject currentLeftProp = Instantiate(propagation, propParent);
                        currentLeftProp.transform.position += new Vector3(-i * 2, 0, 0);
                    }

                    if(currenthitLeft.GetComponent<scr_Bomb>() != null)
                    {
                        currenthitLeft.GetComponent<scr_Bomb>().StopAllCoroutines();
                        currenthitLeft.GetComponent<scr_Bomb>().Explosion();
                    }
                }
                else
                {
                    GameObject currentLeftProp = Instantiate(propagation, propParent);
                    currentLeftProp.transform.position += new Vector3(-i * 2, 0, 0); 
                }
            }
            

            if(canRight)
            {
                if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit hitRight,i * 2, mask))
                {
                    canRight = false;
                    GameObject currenthitRight = hitRight.transform.parent.parent.gameObject;
                    if (currenthitRight.GetComponent<Scr_Block_Breakable>() != null)
                    {
                        currenthitRight.GetComponent<Scr_Block_Breakable>().Explode();
                        GameObject currentLeftProp = Instantiate(propagation, propParent);
                        currentLeftProp.transform.position += new Vector3(i * 2, 0, 0);
                    }

                    if (currenthitRight.GetComponent<scr_Bomb>() != null)
                    {
                        currenthitRight.GetComponent<scr_Bomb>().StopAllCoroutines();
                        currenthitRight.GetComponent<scr_Bomb>().Explosion();
                    }
                }
                else
                {
                    GameObject currentRightProp = Instantiate(propagation, propParent);
                    currentRightProp.transform.position += new Vector3(i * 2, 0, 0);
                }
            }
            

            if(canForward)
            {
                if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitForward,i* 2, mask))
                {
                    canForward = false;
                    GameObject currenthitForward = hitForward.transform.parent.parent.gameObject;
                    if (currenthitForward.GetComponent<Scr_Block_Breakable>() != null)
                    {
                        currenthitForward.GetComponent<Scr_Block_Breakable>().Explode();
                        GameObject currentLeftProp = Instantiate(propagation, propParent);
                        currentLeftProp.transform.position += new Vector3(0, 0, i * 2);
                    }

                    if (currenthitForward.GetComponent<scr_Bomb>() != null)
                    {
                        currenthitForward.GetComponent<scr_Bomb>().StopAllCoroutines();
                        currenthitForward.GetComponent<scr_Bomb>().Explosion();
                    }
                }
                else
                {
                    GameObject currentForwardProp = Instantiate(propagation, propParent);
                    currentForwardProp.transform.position += new Vector3(0, 0, i * 2);
                }
            }
            

            if(canBack)
            {
                if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hitBack,i* 2, mask))
                {
                    canBack = false;
                    GameObject currenthitBack = hitBack.transform.parent.parent.gameObject;
                    if (currenthitBack.GetComponent<Scr_Block_Breakable>() != null)
                    {
                        currenthitBack.GetComponent<Scr_Block_Breakable>().Explode();
                        GameObject currentLeftProp = Instantiate(propagation, propParent);
                        currentLeftProp.transform.position += new Vector3(0, 0, -i * 2);
                    }

                    if (currenthitBack.GetComponent<scr_Bomb>() != null)
                    {
                        currenthitBack.GetComponent<scr_Bomb>().StopAllCoroutines();
                        currenthitBack.GetComponent<scr_Bomb>().Explosion();
                    }
                }
                else
                {
                    GameObject currentBackProp = Instantiate(propagation, propParent);
                    currentBackProp.transform.position += new Vector3(0, 0,-i * 2);
                }
            }
        }
    }

}
