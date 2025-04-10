using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Scr_Bomb_Propagation : MonoBehaviour
{
    public int range;

    public LayerMask mask;

    private bool canLeft, canRight, canForward, canBack;

    public GameObject propagation;

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
                    GameObject currentLeftHit = hitLeft.transform.gameObject;

                    while (currentLeftHit.transform.parent.parent != null) currentLeftHit = currentLeftHit.transform.parent.gameObject;

                    if (currentLeftHit.GetComponent<Scr_Block_Breakable>() != null)
                    {
                        currentLeftHit.GetComponent<Scr_Block_Breakable>().Explode();
                        currentLeftHit = Instantiate(propagation, transform);
                        currentLeftHit.transform.localPosition = Vector3.zero;
                        currentLeftHit.transform.position += new Vector3(-i * 2, 0, 0);
                    }

                    if (currentLeftHit.GetComponent<Scr_Bomb>() != null)
                    {
                        currentLeftHit.GetComponent<Scr_Bomb>().StopAllCoroutines();
                        currentLeftHit.GetComponent<Scr_Bomb>().Explosion();
                    }
                }
                else
                {
                    GameObject currentLeftProp = Instantiate(propagation, transform);
                    currentLeftProp.transform.position += new Vector3(-i * 2, 0, 0); 
                }
            }
            

            if(canRight)
            {
                if (Physics.Raycast(transform.position, Vector3.right, out RaycastHit hitRight,i * 2, mask))
                {
                    canRight = false;
                    GameObject currentRightHit = hitRight.transform.gameObject;

                    while (currentRightHit.transform.parent.parent != null) currentRightHit = currentRightHit.transform.parent.gameObject;

                    if (currentRightHit.GetComponent<Scr_Block_Breakable>() != null)
                    {
                        currentRightHit.GetComponent<Scr_Block_Breakable>().Explode();
                        currentRightHit = Instantiate(propagation, transform);
                        currentRightHit.transform.localPosition = Vector3.zero;
                        currentRightHit.transform.position += new Vector3(i * 2, 0, 0);
                    }

                    if (currentRightHit.GetComponent<Scr_Bomb>() != null)
                    {
                        currentRightHit.GetComponent<Scr_Bomb>().StopAllCoroutines();
                        currentRightHit.GetComponent<Scr_Bomb>().Explosion();
                    }
                }
                else
                {
                    GameObject currentRightProp = Instantiate(propagation, transform);
                    currentRightProp.transform.position += new Vector3(i * 2, 0, 0);
                }
            }
            

            if(canForward)
            {
                if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitForward,i* 2, mask))
                {
                    canForward = false;
                    GameObject currentForwardHit = hitForward.transform.gameObject;

                    while (currentForwardHit.transform.parent.parent != null) currentForwardHit = currentForwardHit.transform.parent.gameObject;

                    if (currentForwardHit.GetComponent<Scr_Block_Breakable>() != null)
                    {
                        currentForwardHit.GetComponent<Scr_Block_Breakable>().Explode();
                        currentForwardHit = Instantiate(propagation, transform);
                        currentForwardHit.transform.localPosition = Vector3.zero;
                        currentForwardHit.transform.position += new Vector3(0, 0, i * 2);
                    }

                    if (currentForwardHit.GetComponent<Scr_Bomb>() != null)
                    {
                        currentForwardHit.GetComponent<Scr_Bomb>().StopAllCoroutines();
                        currentForwardHit.GetComponent<Scr_Bomb>().Explosion();
                    }
                }
                else
                {
                    GameObject currentForwardProp = Instantiate(propagation, transform);
                    currentForwardProp.transform.position += new Vector3(0, 0, i * 2);
                }
            }
            

            if(canBack)
            {
                if (Physics.Raycast(transform.position, Vector3.back, out RaycastHit hitBack,i * 2, mask))
                {
                    canBack = false;
                    GameObject currentBackHit = hitBack.transform.gameObject ;
                    
                    while(currentBackHit.transform.parent.parent != null) currentBackHit = currentBackHit.transform.parent.gameObject;

                    if (currentBackHit.GetComponent<Scr_Block_Breakable>() != null)
                    {
                        currentBackHit.GetComponent<Scr_Block_Breakable>().Explode();
                        currentBackHit = Instantiate(propagation, transform);
                        currentBackHit.transform.localPosition = Vector3.zero;
                        currentBackHit.transform.position += new Vector3(0, 0, -i * 2);

                        Debug.Log("block");
                    }

                    if (currentBackHit.GetComponent<Scr_Bomb>() != null)
                    {
                        currentBackHit.GetComponent<Scr_Bomb>().StopAllCoroutines();
                        currentBackHit.GetComponent<Scr_Bomb>().Explosion();
                        Debug.Log("Boum");
                    }
                    Debug.Log("Nothing");
                }
                else
                {
                    GameObject currentBackProp = Instantiate(propagation, transform);
                    currentBackProp.transform.position += new Vector3(0, 0,-i * 2);
                }
            }
        }
    }
}
