using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using System;

public class GoblinRock : MonoBehaviour
{
    public GameObject rock;
    public Transform rockPos;
    private GameObject enemy;
    private GameObject player;
    private Animator enemyAnimator;
    public bool moving;



    private float timer;
    public float force;

    private void Start()
    {
        enemy = gameObject;
        player = GameObject.Find("Player");
        enemyAnimator = gameObject.GetComponent<Animator>();
    }

    private float FindRadius(float x, float y)
    {
        return Mathf.Sqrt((x * x) + (y * y));
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            initiateThrow();
        }
    }

    private void initiateThrow()
    {
        float distToPlayer = FindRadius(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y);

        if (distToPlayer <= 2)
        {
            enemyAnimator.SetBool("isMoving", false);
            enemyAnimator.SetTrigger("isAttacking");
            //ChangeMovingStatus();
           // Throw();
        }
    }

    private void Throw()
    {
        Instantiate(rock, enemy.transform.position, Quaternion.identity);
    } 

    public void ChangeMovingStatus()
    {
        if (moving)
        {
            enemyAnimator.SetBool("isMoving", moving);
        }
        else
        {
            enemyAnimator.SetBool("isMoving", moving);
        }
    }
}
