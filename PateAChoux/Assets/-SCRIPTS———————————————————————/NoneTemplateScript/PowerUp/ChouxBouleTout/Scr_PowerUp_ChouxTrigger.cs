using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_PowerUp_ChouxTrigger : MonoBehaviour
{
    [HideInInspector] private int id;

    public LayerMask mask;
    private bool canPush;

    private void Start()
    {
        id = GetComponentInParent<PlayerMove>().playerID;
        canPush = true;
        
    }

    private float distanceX , distanceZ ;

    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerMove>();

        if (player != null && id != player.playerID)
        {
            currentPlayer = player.gameObject;
            pushWantedPos = player.transform.position;
            player.CanRun = false;
            SetDir();

            player.transform.DOMove(pushWantedPos, 1).SetEase(Ease.InCubic).OnComplete(ActiveMove); // La ligne est appelée mais le DoMove ne fonctionne pas   
        }
    }

    [HideInInspector] private GameObject currentPlayer;

    void ActiveMove()
    {
        currentPlayer.GetComponent<PlayerMove>().CanRun = true;
        Debug.Log("Le joueur se déplace");
    }

    private Vector3 pushWantedPos;

    void SetDir()
    {
        distanceX = Vector3.Distance(new Vector3(currentPlayer.transform.position.x, 0, 0), new Vector3(transform.position.x, 0, 0));
        distanceZ = Vector3.Distance(new Vector3(0, 0, currentPlayer.transform.position.z), new Vector3(0, 0, transform.position.z));

        if (distanceX < distanceZ)
        {
            if (currentPlayer.transform.position.z > transform.position.z) Push(Vector3.back * 2);
            else Push(Vector3.forward * 2);
        }
        else
        {
            if (currentPlayer.transform.position.x > transform.position.x) Push(Vector3.left * 2);
            else Push(Vector3.right);
        }
    }

    void Push(Vector3 dir)
    {
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(pushWantedPos, dir, out RaycastHit hitBack, 2, mask))
            {
                canPush = false;
            }
            else
            {
                pushWantedPos += dir;
            }
        }
    }

    
}
