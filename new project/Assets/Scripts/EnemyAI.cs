using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Unity.Mathematics;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    private Transform playerTransform;
    public Rigidbody2D player;
    public Rigidbody2D enemy;
    public float movespeed = 1f;

    SpriteRenderer enemySpriteRenderer;
    Animator animator;

    private double[] interestMap = new double[8];
    private double[] avoidanceMap = new double[8];
    double[] weightedMap = new double[8];
    private Vector3[] dirArray;
    
    public double FindRadius(double x,double y)
    {
        double r = math.sqrt((x*x) + (y*y));
        return r;
    }

    private double normaliseVector(double x, double y)
    {
        return math.sqrt((x*x) + (y*y));
    }

    /*
     * Calculating the values of each elements of the interest map based on the dot product between the directional vectors from enemy to the player and the directional vectors
       of the 8 directions
     * Ranges from -1 to 1 where -1 is directly opposite and 1 means that the respective direction is parallel to the directional vector from enemy to player
     */
    private void populateIntMap(double x_toTarget, double y_toTarget, double radius)
    {
        double componentOfDiag = math.sqrt((radius * radius) / 2);
        interestMap[0] = ((0 * x_toTarget) + (radius * y_toTarget)) / (radius * normaliseVector(x_toTarget, y_toTarget));    //North 
        interestMap[1] = ((componentOfDiag * x_toTarget) + (componentOfDiag * y_toTarget)) / (radius * normaliseVector(x_toTarget, y_toTarget));    //North-East 
        interestMap[2] = ((radius * x_toTarget) + (0 * y_toTarget)) / (radius * normaliseVector(x_toTarget, y_toTarget));    //East 
        interestMap[3] = ((componentOfDiag * x_toTarget) + (-componentOfDiag * y_toTarget)) / (radius * normaliseVector(x_toTarget, y_toTarget));    //South-East
        interestMap[4] = ((0 * x_toTarget) + (-radius * y_toTarget)) / (radius * normaliseVector(x_toTarget, y_toTarget));    //South
        interestMap[5] = ((-componentOfDiag * x_toTarget) + (-componentOfDiag * y_toTarget)) /  (radius * normaliseVector(x_toTarget, y_toTarget));    //South-West
        interestMap[6] = ((-radius * x_toTarget) + (0 * y_toTarget)) / (radius * normaliseVector(x_toTarget, y_toTarget));    //West
        interestMap[7] = ((-componentOfDiag * x_toTarget) + (componentOfDiag * y_toTarget)) / (radius * normaliseVector(x_toTarget, y_toTarget));    //North-West

    }

    /*
     * Calculating the values of each elements of the avoidance map based on how far the enemy is from the player. Higher value means further
     * ranges from 1 to 0 where 0 is the closest (0.8) and 1 means furthest
     */
    private void populateAvoidMap()
    {
        Vector2 sizeBox = new Vector2(0.028f, 0.028f); 
        RaycastHit2D hitUp = Physics2D.BoxCast(transform.position,sizeBox,0, Vector3.up, 1f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitRight = Physics2D.BoxCast(transform.position, sizeBox, 0, Vector3.right, 1f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitDown = Physics2D.BoxCast(transform.position, sizeBox, 0, Vector3.down, 1f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitLeft = Physics2D.BoxCast(transform.position, sizeBox, 0, Vector3.left, 1f, LayerMask.GetMask("Obstacles"));
        
        Vector3 dirNE = new Vector3(1f, 1f, 0f).normalized;
        Vector3 dirSE = new Vector3(1f,-1f, 0f).normalized;
        Vector3 dirSW = new Vector3(-1f, -1f, 0f).normalized;
        Vector3 dirNW = new Vector3(-1f, 1f, 0f).normalized;

        RaycastHit2D hitNE = Physics2D.BoxCast(transform.position, sizeBox, 0, dirNE, 1f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitSE = Physics2D.BoxCast(transform.position, sizeBox, 0, dirSE, 1f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitSW = Physics2D.BoxCast(transform.position, sizeBox, 0, dirSE, 1f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitNW = Physics2D.BoxCast(transform.position, sizeBox, 0, dirNW, 1f, LayerMask.GetMask("Obstacles"));

        if (hitUp.collider)
        {
            avoidanceMap[0] = 1 - (hitUp.distance / 0.8);
            Debug.Log(hitUp);
        }
        else
        {
            avoidanceMap[0] = -1;   //it is assigned -1 to make the difference more drastic for testing purposes
           // Debug.Log(hitUp);
        }
        if (hitRight.collider)
        {
            avoidanceMap[2] = 1 - (hitRight.distance / 0.8);
        }
        else
        {
            avoidanceMap[2] = -1;
        }
        if (hitDown.collider)
        {
            avoidanceMap[4] = 1 - (hitDown.distance / 0.8);
        }
        else
        {
            avoidanceMap[4] = -1;
        }
        if (hitLeft.collider)
        {
            avoidanceMap[6] = 1 - (hitLeft.distance / 0.8);
        }
        else
        {
            avoidanceMap[6] = -1;
        }
        if (hitNE.collider)
        {
            avoidanceMap[1] = 1 - (hitNE.distance / 0.8);
        }
        else
        {
            avoidanceMap[1] = -1;
        }
        if (hitSE.collider)
        {
            avoidanceMap[3] = 1 - (hitSE.distance / 0.8);
        }
        else
        {
            avoidanceMap[3] = -1;
        }
        if (hitSW.collider)
        {
            avoidanceMap[5] = 1 - (hitSW.distance / 0.8);
        }
        else
        {
            avoidanceMap[5] = -1;
        }
        if (hitNW.collider)
        {
            avoidanceMap[7] = 1 - (hitNW.distance / 0.8);
        }
        else
        {
            avoidanceMap[7] = -1;
        }
    }

    /*
     * To get the resultant map
     */
    private void weighTheMaps()
    {
        for (int x = 0; x < 8; x+= 1)
        {
            weightedMap[x] = interestMap[x] - avoidanceMap[x];
        }
    }
    private void Start()
    {
        enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
        animator = enemy.GetComponent<Animator>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        dirArray = new Vector3[8];
        dirArray[0] = Vector3.up;
        dirArray[1] = new Vector3(1f, 1f, 0f).normalized;
        dirArray[2] = Vector3.right;
        dirArray[3] = new Vector3(1f, -1f, 0f).normalized;
        dirArray[4] = Vector3.down;
        dirArray[5] = new Vector3(-1f, -1f, 0f).normalized;
        dirArray[6] = Vector3.left;
        dirArray[7] = new Vector3(-1f, 1f, 0f).normalized;
    }

    private void Update()
    {
        if (animator.GetBool("alive") == true)
        {
            followPlayer();
        }
    }

    private Vector3 findProjection(double x, double y, int index)
    {
        Vector3 result = new Vector3();
        result.x = (float)(dirArray[index].x * 0.4);
        result.y = (float)(dirArray[index].y * 0.4);
        result.z = (float)dirArray[index].z;
        return result;
    }

    private void followPlayer()
    {
        double x_diff = player.transform.position.x - gameObject.transform.position.x;
        double y_diff = player.transform.position.y - gameObject.transform.position.y;

        double r = FindRadius(x_diff, y_diff);
        if (r <= 5 && r >= 0.4)
        {
            Vector2 enemy_path = new Vector2((float)(x_diff / r), (float)(y_diff / r));
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemySpriteRenderer.flipX = false;
            }
            populateIntMap(x_diff, y_diff, 5);
            populateAvoidMap();
            weighTheMaps();

            int indexOfMax = Array.IndexOf(weightedMap, weightedMap.Max());

            enemy_path = findProjection(x_diff, y_diff, indexOfMax);

            enemy.MovePosition(enemy.position + enemy_path * movespeed * Time.fixedDeltaTime);
        }
    }


}
