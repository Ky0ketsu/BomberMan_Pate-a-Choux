using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TerrainPlacementAnimation : MonoBehaviour
{
    [SerializeField] private float timerBeforePlacement;

    private void Start()
    {
        transform.localScale = Vector3.zero;
        timerBeforePlacement = (transform.localPosition.x + transform.localPosition.z) / 20 + 1.5f;
        StartCoroutine(AnimationPlacement());
    }

    IEnumerator AnimationPlacement()
    {
        yield return new WaitForSeconds(timerBeforePlacement);
        transform.DOScale(Vector3.one, 2).SetEase(Ease.OutBounce);
    }


}
