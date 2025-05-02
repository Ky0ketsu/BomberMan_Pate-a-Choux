using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Player_Sprite : MonoBehaviour
{
    public Sprite[] playerSprite = new Sprite[4];
    public Transform graphics;

    public void Start()
    {
        graphics.GetComponent<SpriteRenderer>().sprite = playerSprite[transform.GetComponent<PlayerMove>().playerID];
    }
}
