using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Scr_Menu_Victory : MonoBehaviour
{
    public TextMeshProUGUI tmpro;
    public GameObject player;
    public Sprite[] playerSprite = new Sprite[4];
    [SerializeField] private GameObject[] slotPlayer;
    [SerializeField] private int currentWinner;
    [HideInInspector] private GameObject[] currentLoser;

    public void Victory()
    {
        slotPlayer = new GameObject[4];
        for (int i = 0; i < slotPlayer.Length; i++) slotPlayer[i] = GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(i).gameObject;
        currentLoser = new GameObject[3];
        slotPlayer[3].GetComponentInChildren<Image>().sprite = playerSprite[GameObject.Find("ActivePlayerParent").transform.GetChild(0).GetComponent<PlayerMove>().playerID];
        slotPlayer[3].GetComponentInChildren<TextMeshProUGUI>().text = Scr_ManagerPlayer.acces.win[currentWinner].ToString();


        int currentloserNum = 0;
        for (int i = 0; i < 4; i++)
        {
            if (i != currentWinner)
            {
                currentloserNum++;
                if(currentloserNum <= GameObject.Find("UnActivePlayerParent").transform.childCount)
                {
                    currentLoser[currentloserNum-1] = GameObject.Find("UnActivePlayerParent").transform.GetChild(currentloserNum-1).gameObject;
                    Debug.Log("un perdant");
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (currentLoser[i] != null)
            {
                slotPlayer[i].GetComponentInChildren<Image>().sprite = playerSprite[currentLoser[i].GetComponent<PlayerMove>().playerID];
                slotPlayer[i].GetComponentInChildren<TextMeshProUGUI>().text = Scr_ManagerPlayer.acces.win[currentLoser[i].GetComponent<PlayerMove>().playerID].ToString();
            }
            else
            {
                slotPlayer[i].GetComponentInChildren<Image>().enabled = false;
                slotPlayer[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }

    }
    
}
