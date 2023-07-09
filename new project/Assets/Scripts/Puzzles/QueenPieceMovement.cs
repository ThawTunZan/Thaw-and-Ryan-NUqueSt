using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPieceMovement : MonoBehaviour
{
    private SpriteRenderer playerRenderer;

    void Start()
    {
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    private void MoveLeft()
    {
        float distX = transform.position.x - (float)0.16;
        if (distX > 0.23) 
        {
            transform.position = new Vector2(distX, transform.position.y);
        }
    }

    private void MoveRight()
    {
        float distX = transform.position.x + (float)0.16;
        if (distX < 1.13)
        {
            transform.position = new Vector2(distX, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SwordAttack" && collision.gameObject.name == "Sword Side Hit Box")
        {
            if (playerRenderer.flipX)
            {
                MoveLeft();
            }
            else if (!playerRenderer.flipX)
            {
                MoveRight();
            }
        }
    }
}
