using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Menu_Defeat : MonoBehaviour
{
    [SerializeField] private GameObject[] player = new GameObject[4];
    [SerializeField] private Transform playerList;

    public void Defeat()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i] = playerList.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().gameObject;
            player[i].GetComponent<TextMeshProUGUI>().text = Scr_ManagerPlayer.acces.win[i].ToString();
        }
    }
}
