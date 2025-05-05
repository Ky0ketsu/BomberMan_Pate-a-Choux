
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Scr_Bomb_Frozen : MonoBehaviour
{
    public float distanceX, distanceZ;
    public int pushForce;
    public bool canPush;
    public Vector3 offset;
    public LayerMask mask;

    public void Start()
    {
        StartCoroutine(DelayBeforeCanPush(2));
    }

    IEnumerator DelayBeforeCanPush(float time)
    {
        yield return new WaitForSeconds(time);
        canPush = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerMove>() && canPush)
        {
            distanceX = Vector3.Distance(new Vector3(other.transform.position.x, 0, 0), new Vector3(transform.position.x, 0, 0));
            distanceZ = Vector3.Distance(new Vector3(0, 0, other.transform.position.z), new Vector3(0, 0, transform.position.z));

            if (distanceX < distanceZ)
            {
                if (other.transform.position.z > transform.position.z) Push(new Vector3(0, 0, -2));
                else Push(new Vector3(0, 0, 2));
            }else
            {
                if (other.transform.position.x > transform.position.x) Push(new Vector3(-2,0,0));
                else Push(new Vector3(2,0, 0));
            }

        }

        if(other.GetComponentInParent<Scr_Wall_Properties>() != null)
        {
            transform.DOKill();
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), new Vector3(0, -1, 0), out RaycastHit hit, Mathf.Infinity, mask))
            {
                Transform currentCase = hit.transform.GetChild(0);
                Debug.Log(currentCase);

                transform.position = currentCase.position + offset;
            }
        }
    }

    private void Push(Vector3 dir)
    {
        if(Physics.Raycast(transform.position, dir, out RaycastHit hit, 2, mask))
        {
        
        }
        else
        {
            transform.DOMove(transform.position + dir * pushForce, 1.5f).SetEase(Ease.OutExpo);
            canPush = false;
            StartCoroutine(DelayBeforeCanPush(1.5f));
        }

    }
}
