using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scr_Animation_PateAChoux : MonoBehaviour
{
    [SerializeField] private Transform rightArm, leftArm, body;

    [HideInInspector] private GameObject[] players = new GameObject[4];

    private void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = Scr_ManagerPlayer.acces.activePlayers[i];
        }
        EVENTS.OnStartAnimation += playerSpawnAnimation;
    }

    private void OnDestroy()
    {
        EVENTS.OnStartAnimation -= playerSpawnAnimation;
    }

    void AnimationLoop()
    {
        transform.DOMoveY(transform.position.y + 3, 6).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        leftArm.DOLocalMoveX(leftArm.localPosition.x + 2, 5).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        rightArm.DOLocalMoveX(rightArm.localPosition.x - 2, 4).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }


    public void playerSpawnAnimation()
    {
        for (int i = 0; i < Scr_ManagerPlayer.acces.activePlayers.Length; i++)
        {
            if (Scr_ManagerPlayer.acces.activePlayers[i] != null)
            {
                Invoke("SpawnSequence", i);
            }
            if (Scr_ManagerPlayer.acces.activePlayers[i] == null)
            {
                Invoke("AnimationLoop", i + 1);
            }
        }
        
    }

    [HideInInspector] int currentAnimation;

    private void SpawnSequence()
    {
        if(currentAnimation == 0) rightArm.transform.DOMoveZ(Scr_ManagerPlayer.acces.activePlayers[0].transform.position.z, 1f).SetEase(Ease.InOutCubic).SetLoops(1 , LoopType.Yoyo);
        if(currentAnimation == 1) leftArm.transform.DOMoveZ(Scr_ManagerPlayer.acces.activePlayers[1].transform.position.z, 1f).SetEase(Ease.InOutCubic).SetLoops(1, LoopType.Yoyo);
        if(currentAnimation == 2) leftArm.transform.DOKill(); leftArm.transform.DOMoveZ(Scr_ManagerPlayer.acces.activePlayers[2].transform.position.z, 1f).SetEase(Ease.InOutCubic).SetLoops(1, LoopType.Yoyo);
        if(currentAnimation == 3) rightArm.transform.DOMoveZ(Scr_ManagerPlayer.acces.activePlayers[3].transform.position.z, 1f).SetEase(Ease.InOutCubic).SetLoops(1, LoopType.Yoyo);

        currentAnimation++;
    }


}
