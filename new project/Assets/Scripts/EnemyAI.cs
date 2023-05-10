using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Unity.Mathematics;
using System;

public class EnemyAI : MonoBehaviour
{
    public Rigidbody2D player;
    public float movespeed = 1f;

    public Rigidbody2D enemy;

    Vector2 enemyMovement;

    SpriteRenderer enemySpriteRenderer;
    Animator animator;
    public float FindRadius(float x,float y)
    {
        float r = math.sqrt((x*x) + (y*y));
        return r;
    }

    private void Start()
    {
        enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
        animator = enemy.GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator.GetBool("alive") == true)
        {
            followPlayer();
        }
    }

    private void followPlayer()
    {
        float x_diff = gameObject.transform.position.x - player.transform.position.x;
        float y_diff = gameObject.transform.position.y - player.transform.position.y;

        float r = FindRadius(x_diff, y_diff);
        if (r <= 5 && r >= 1)
        {
            Debug.Log("aggro");
            Vector2 enemy_path = new Vector2(x_diff / r, y_diff / r);

            if (gameObject.transform.position.x > player.transform.position.x)
            {
                enemy_path.x *= -1;
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemy_path.x = math.abs(enemy_path.x);
                enemySpriteRenderer.flipX = false;
            }
            if (gameObject.transform.position.y > player.transform.position.y)
            {
                enemy_path.y *= -1;
            }
            else
            {
                enemy_path.y = math.abs(enemy_path.y);
            }
            enemy.MovePosition(enemy.position + enemy_path * movespeed * Time.fixedDeltaTime);
        }
    }


}
