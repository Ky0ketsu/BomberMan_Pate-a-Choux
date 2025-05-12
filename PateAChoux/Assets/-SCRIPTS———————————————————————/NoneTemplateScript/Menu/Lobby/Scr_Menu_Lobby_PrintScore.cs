using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_Menu_Lobby_PrintScore : MonoBehaviour
{
    [SerializeField] private GameObject[] player = new GameObject[4];

    public void PrintScore()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i] = transform.Find("PlayerSlotList").GetChild(i).GetComponentInChildren<TextMeshProUGUI>().gameObject;
            player[i].GetComponent<TextMeshProUGUI>().text = Scr_ManagerPlayer.acces.win[i].ToString();
        }
    }
}
