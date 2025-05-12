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
    public Sprite[] players = new Sprite[4];

    public void Victory()
    {
        var winner = GameObject.Find("ActivePlayerParent").transform.GetChild(0);
        tmpro.text = "Le " + winner.name + " a gagné";

        var winnerID = winner.GetComponent<PlayerMove>().playerID;
        PrintWinner(winnerID);
    }

    private void PrintWinner(int ID)
    {
        player.GetComponent<Image>().sprite = players[ID];
    }
}
