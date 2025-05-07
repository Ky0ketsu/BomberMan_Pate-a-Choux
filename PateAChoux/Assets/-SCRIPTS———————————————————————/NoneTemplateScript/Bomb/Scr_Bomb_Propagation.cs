
using UnityEngine;


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
                if (Physics.Raycast(transform.position + Vector3.up, Vector3.left, out RaycastHit hitLeft,i * 2, mask))
                {
                    canLeft = false;
                    GameObject currentLeftHit = hitLeft.transform.gameObject;

                    if (currentLeftHit.GetComponentInParent<Scr_Block_Breakable>() != null)
                    {
                        currentLeftHit.GetComponentInParent<Scr_Block_Breakable>().Explode();
                        currentLeftHit = Instantiate(propagation, transform);
                        currentLeftHit.transform.localPosition = Vector3.zero;
                        currentLeftHit.transform.position += new Vector3(-i * 2, 0, 0);
                    }

                    if (currentLeftHit.GetComponentInParent<Scr_Bomb>() != null)
                    {
                        currentLeftHit.GetComponentInParent<Scr_Bomb>().StopAllCoroutines();
                        currentLeftHit.GetComponentInParent<Scr_Bomb>().Explosion();
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
                if (Physics.Raycast(transform.position + Vector3.up, Vector3.right, out RaycastHit hitRight,i * 2, mask))
                {
                    canRight = false;
                    GameObject currentRightHit = hitRight.transform.gameObject;

                    if (currentRightHit.GetComponentInParent<Scr_Block_Breakable>() != null)
                    {
                        currentRightHit.GetComponentInParent<Scr_Block_Breakable>().Explode();
                        currentRightHit = Instantiate(propagation, transform);
                        currentRightHit.transform.localPosition = Vector3.zero;
                        currentRightHit.transform.position += new Vector3(i * 2, 0, 0);
                    }

                    if (currentRightHit.GetComponentInParent<Scr_Bomb>() != null)
                    {
                        currentRightHit.GetComponentInParent<Scr_Bomb>().StopAllCoroutines();
                        currentRightHit.GetComponentInParent<Scr_Bomb>().Explosion();
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
                if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, out RaycastHit hitForward,i* 2, mask))
                {
                    canForward = false;
                    GameObject currentForwardHit = hitForward.transform.gameObject;

                    if (currentForwardHit.GetComponentInParent<Scr_Block_Breakable>() != null)
                    {
                        currentForwardHit.GetComponentInParent<Scr_Block_Breakable>().Explode();
                        currentForwardHit = Instantiate(propagation, transform);
                        currentForwardHit.transform.localPosition = Vector3.zero;
                        currentForwardHit.transform.position += new Vector3(0, 0, i * 2);
                    }

                    if (currentForwardHit.GetComponentInParent<Scr_Bomb>() != null)
                    {
                        currentForwardHit.GetComponentInParent<Scr_Bomb>().StopAllCoroutines();
                        currentForwardHit.GetComponentInParent<Scr_Bomb>().Explosion();
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
                if (Physics.Raycast(transform.position + Vector3.up, Vector3.back, out RaycastHit hitBack,i * 2, mask))
                {
                    canBack = false;
                    GameObject currentBackHit = hitBack.transform.gameObject ;

                    if (currentBackHit.GetComponentInParent<Scr_Block_Breakable>() != null)
                    {
                        currentBackHit.GetComponentInParent<Scr_Block_Breakable>().Explode();
                        currentBackHit = Instantiate(propagation, transform);
                        currentBackHit.transform.localPosition = Vector3.zero;
                        currentBackHit.transform.position += new Vector3(0, 0, -i * 2);
                    }

                    if (currentBackHit.GetComponentInParent<Scr_Bomb>() != null)
                    {
                        Debug.Log("Booooooooooooooooooooooooooooooooooooo");
                        currentBackHit.GetComponentInParent<Scr_Bomb>().StopAllCoroutines();
                        currentBackHit.GetComponentInParent<Scr_Bomb>().Explosion();
                    }
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
