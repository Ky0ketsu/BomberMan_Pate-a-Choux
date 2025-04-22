using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Scr_Block_Falling : MonoBehaviour
{
    public float time;

    void Start()
    {
        transform.DOMove(new Vector3(transform.position.x, 0, transform.position.z), time);
    }

    
}
