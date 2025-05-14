using System.Collections;
using UnityEngine;

public class Scr_MortSubite : MonoBehaviour
{
    [SerializeField] float timer;
    float timerMax;
    bool mortSubiteDemarree = false;

    private void Awake()
    {
        timerMax = timer;
        EVENTS.OnGameOver += ResetTimer;
    }
    private void OnDestroy()
    {
        EVENTS.OnGameOver -= ResetTimer;
    }

    void ResetTimer()
    {
        timer = timerMax;
    }


    void Update()
    {
        if (GAME.MANAGER.CurrentState==State.gameplay)
        {
            timer -= Time.deltaTime;
            if (timer<0 && mortSubiteDemarree==false)StartMortSubite();
        }
    }

    public void StartMortSubite()
    {
        mortSubiteDemarree = true;
        EVENTS.InvokeMortSubite();
    }

} // FIN DU SCRIPT
