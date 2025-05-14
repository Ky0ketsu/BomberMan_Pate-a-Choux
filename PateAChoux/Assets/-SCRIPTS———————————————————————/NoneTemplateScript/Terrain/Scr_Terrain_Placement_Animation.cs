using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scr_Terrain_Placement_Animation : MonoBehaviour
{
    [SerializeField] private float timerBeforePlacement;
    Transform graphics => transform.Find("Graphics"); 
    AudioSource BlocApparitionAudioSource;

    private void Start()
    {
        graphics.localScale = Vector3.zero;
        timerBeforePlacement = (transform.localPosition.x + transform.localPosition.z) / 20 + 1.5f;
        StartCoroutine(AnimationPlacement());
        BlocApparitionAudioSource = GetComponent<AudioSource>();
    }

    IEnumerator AnimationPlacement()
    {
        yield return new WaitForSeconds(timerBeforePlacement);
        graphics.DOKill();
        graphics.DOScale(Vector3.one, 2).SetEase(Ease.OutBounce).From(0);
        BlocApparitionAudioSource.Play();
    }


}
