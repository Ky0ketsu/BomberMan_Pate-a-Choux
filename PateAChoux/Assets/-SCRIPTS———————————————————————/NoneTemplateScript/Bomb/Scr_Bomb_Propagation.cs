
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
            if(canLeft) Propagation(Vector3.left, i);
            if(canRight) Propagation(Vector3.right, i);
            if(canForward) Propagation(Vector3.forward, i);
            if(canBack) Propagation(Vector3.back, i);
        }
    }

    private void Propagation(Vector3 dir, int distance)
    {
        if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit hitBack, distance * 2, mask))
        {
            if(dir == Vector3.forward) canForward = false; if (dir == Vector3.back) canBack = false; if (dir == Vector3.right) canRight = false; if (dir == Vector3.left) canLeft = false;

            GameObject currentBackHit = hitBack.transform.gameObject;

            if (currentBackHit.GetComponentInParent<Scr_Block_Breakable>() != null)
            {
                currentBackHit.GetComponentInParent<Scr_Block_Breakable>().Explode();
                currentBackHit = Instantiate(propagation, transform);
                currentBackHit.transform.localPosition = Vector3.zero;
                currentBackHit.transform.position += dir * 2 * distance;
            }

            if (currentBackHit.GetComponentInParent<Scr_Bomb>() != null)
            {
                currentBackHit.GetComponentInParent<Scr_Bomb>().StopAllCoroutines();
                currentBackHit.GetComponentInParent<Scr_Bomb>().Explosion();
            }
        }
        else
        {
            GameObject currentBackProp = Instantiate(propagation, transform);
            currentBackProp.transform.position += dir * 2 * distance;
        }
    }
}
