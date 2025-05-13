using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Scr_Block_Falling : MonoBehaviour
{
    public float time;
    public GameObject colliders;
    public GameObject graphics;
    public GameObject block;
    bool fallen = false;

    [SerializeField] private GameObject particule;

    public void Awake()
    {
        colliders.SetActive(true);
        block.SetActive(false);
    }

    public void fallBlock()
    {
        if (fallen) return;
        fallen = true;
        block.SetActive(true);
        graphics.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InCubic);
        graphics.transform.DORotate(new Vector3(0, block.transform.localEulerAngles.y + 90, 0), 0.2f).SetEase(Ease.InCubic);
        block.transform.DOMove(block.transform.position + Vector3.up * 2, 0.2f).SetEase(Ease.InOutExpo).OnComplete(FallBlockStep2);
    }

    private void FallBlockStep2()
    {
        block.transform.DOMove(new Vector3(transform.position.x, 0, transform.position.z), 0.3f).SetEase(Ease.InExpo).OnComplete(DelayToDisable);
        graphics.transform.DORotate(new Vector3(0, block.transform.localEulerAngles.y + 90, 0), 0.2f).SetEase(Ease.InCubic);
    }

    private void DelayToDisable()
    {
        Instantiate(particule, block.transform.position, transform.rotation, GameObject.Find("ParticuleParent").transform);
        StartCoroutine(DelCol());
    }

    IEnumerator DelCol()
    {
        yield return new WaitForSeconds(0.5f);
        colliders.SetActive(false);
    }
}
