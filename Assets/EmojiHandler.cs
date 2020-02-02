using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private int health;
    [SerializeField]
    Sprite[] sprites = new Sprite[6];
    void Start()
    {
        health = GameObject.Find("GameController").GetComponent<GameController>().getHealth();
    }

    // Update is called once per frame
    void Update()
    {

            GetComponent<SpriteRenderer>().sprite = sprites[health - 1];
            health = GameObject.Find("GameController").GetComponent<GameController>().getHealth();
    }
}
