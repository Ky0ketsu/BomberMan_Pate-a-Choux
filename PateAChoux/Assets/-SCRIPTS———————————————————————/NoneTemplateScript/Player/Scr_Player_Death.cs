using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scr_Player_Death : MonoBehaviour
{
    public Transform graphic;
    public Transform colliders;
    static int playerAlive;
    bool alive = true;

    private void Awake()
    {
        EVENTS.OnGameStart += AddAlivePlayer;
        EVENTS.OnGameOver += ResetAlivePlayer;
    }

    void AddAlivePlayer()
    {
        playerAlive++;
    }

    private void ResetAlivePlayer()
    {
        playerAlive = 0;
    }

    public void FallingDeath()
    {
        graphic.DOScale(new Vector3(1.3f, 0.02f, 1.3f), 0.2f);
    }

    public void BombDeath()
    {
        graphic.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce);
    }

    public void Death()
    {
        if (!alive) return;
        alive = false;
        Debug.Log(gameObject.name + " est mort");
        playerAlive--;
        transform.GetComponent<PlayerMove>().CanRun = false;
        colliders.gameObject.SetActive(false);
        //GetComponent<CharacterController>().enabled = false;
        transform.parent = GameObject.Find("UnActivePlayerParent").transform;
    }
}
