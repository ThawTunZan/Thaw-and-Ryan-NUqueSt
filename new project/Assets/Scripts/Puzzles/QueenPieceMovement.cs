using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPieceMovement : MonoBehaviour
{
    private SpriteRenderer playerRenderer;
    private QueenChecker queenChecker;
    private int queenNum;

    void Start()
    {
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        queenChecker = transform.parent.gameObject.GetComponent<QueenChecker>();
        bool parseSuccess = int.TryParse(gameObject.name[gameObject.name.Length - 1].ToString(), out queenNum);
        queenNum--;
        queenChecker.queenPositions[queenNum] = transform.position;
    }

    private void MoveLeft()
    {
        float distX = transform.position.x - (float)0.16;
        if (distX > 0.23) 
        {
            transform.position = new Vector2(distX, transform.position.y);
            queenChecker.queenPositions[queenNum] = transform.position;
        }
    }

    private void MoveRight()
    {
        float distX = transform.position.x + (float)0.16;
        if (distX < 1.13)
        {
            transform.position = new Vector2(distX, transform.position.y);
            queenChecker.queenPositions[queenNum] = transform.position;
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
