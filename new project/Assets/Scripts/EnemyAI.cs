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
    public float movespeed = 0.1f;

    SpriteRenderer enemySpriteRenderer;
    Animator animator;

    private double[] interestMap = new double[8];
    private double[] avoidanceMap = new double[8];
    double[] weightedMap = new double[8];
    private Vector3[] dirArray;

    Vector3 enemy_path;
    public bool obstructed;
    Vector3 lastKnown;
    
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
    private void populateIntMap(double x_toTarget, double y_toTarget, double radius, double r, bool isObstructed)
    {
        double componentOfDiag = math.sqrt((5 * 5) / 2);
        interestMap[0] = ((0 * x_toTarget) + (5 * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //North 
        interestMap[1] = ((componentOfDiag * x_toTarget) + (componentOfDiag * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //North-East 
        interestMap[2] = ((5 * x_toTarget) + (0 * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //East 
        interestMap[3] = ((componentOfDiag * x_toTarget) + (-componentOfDiag * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //South-East
        interestMap[4] = ((0 * x_toTarget) + (-5 * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //South
        interestMap[5] = ((-componentOfDiag * x_toTarget) + (-componentOfDiag * y_toTarget)) /  (5 * normaliseVector(x_toTarget, y_toTarget));    //South-West
        interestMap[6] = ((-5 * x_toTarget) + (0 * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //West
        interestMap[7] = ((-componentOfDiag * x_toTarget) + (componentOfDiag * y_toTarget)) / (5 * normaliseVector(x_toTarget, y_toTarget));    //North-West
        if (r < 0.58 && !isObstructed)
        {
            for (int x = 0; x < 8; x += 1)
            {
                interestMap[x] *= -1;
            }
        }
    }

    /*
     * Calculating the values of each elements of the avoidance map based on how far the enemy is from the player. Higher value means further
     * ranges from 1 to 0 where 0 is the closest (0.8) and 1 means furthest
     */
    private void populateAvoidMap()
    {
        Vector2 sizeBox = new Vector2(0.028f, 0.028f); 
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position,Vector3.up, 0.5f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector3.right, 0.5f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector3.down, 0.5f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector3.left, 0.5f, LayerMask.GetMask("Obstacles"));
        
        Vector3 dirNE = new Vector3(0.5f, 0.5f, 0f).normalized;
        Vector3 dirSE = new Vector3(0.5f,-0.5f, 0f).normalized;
        Vector3 dirSW = new Vector3(-0.5f, -0.5f, 0f).normalized;
        Vector3 dirNW = new Vector3(-0.5f, 0.5f, 0f).normalized;

        RaycastHit2D hitNE = Physics2D.Raycast(transform.position, dirNE, 0.5f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitSE = Physics2D.Raycast(transform.position, dirSE, 0.5f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitSW = Physics2D.Raycast(transform.position, dirSW, 0.5f, LayerMask.GetMask("Obstacles"));
        RaycastHit2D hitNW = Physics2D.Raycast(transform.position, dirNW, 0.5f, LayerMask.GetMask("Obstacles"));

        if (hitUp.collider)
        {
            avoidanceMap[0] = 1 - ((hitUp.distance - 0.075) / 0.5);
          //  Debug.Log(hitUp);
        }
        else
        {
            avoidanceMap[0] = 0; 
        }
        if (hitRight.collider)
        {
            avoidanceMap[2] = 1 - ((hitRight.distance - 0.075) / 0.5);
        }
        else
        {
            avoidanceMap[2] = 0;
        }
        if (hitDown.collider)
        {
            avoidanceMap[4] = 1 - ((hitDown.distance - 0.075) / 0.5);
        }
        else
        {
            avoidanceMap[4] = 0;
        }
        if (hitLeft.collider)
        {
            avoidanceMap[6] = 1 - ((hitLeft.distance - 0.075) / 0.5);
        }
        else
        {
            avoidanceMap[6] = 0;
        }
        if (hitNE.collider)
        {
            avoidanceMap[1] = 1 - ((hitNE.distance - 0.075) / 0.5);
        }
        else
        {
            avoidanceMap[1] = 0;
        }
        if (hitSE.collider)
        {
            avoidanceMap[3] = 1 - ((hitSE.distance - 0.075) / 0.5);
        }
        else
        {
            avoidanceMap[3] = 0;
        }
        if (hitSW.collider)
        {
            avoidanceMap[5] = 1 - ((hitSW.distance - 0.075) / 0.5);
        }
        else
        {
            avoidanceMap[5] = 0;
        }
        if (hitNW.collider)
        {
            avoidanceMap[7] = 1 - ((hitNW.distance - 0.075) / 0.5);
        }
        else
        {
            avoidanceMap[7] = 0;
        }
    }

    private Vector3 weighTheMaps(double x_diff, double y_diff, double radius)
    {
        double xToPlayer = player.transform.position.x - enemy.transform.position.x;
        double yToPlayer = player.transform.position.y - enemy.transform.position.y;
        double r = FindRadius(xToPlayer, yToPlayer);
        Vector3 dirToPlayer = new Vector3((float)xToPlayer, (float)yToPlayer, 0);
        bool isObstructed = Physics2D.Raycast(transform.position, dirToPlayer, (float)r, LayerMask.GetMask("Obstacles"));

        Vector3 resultantVector = new Vector3();
        if ((radius > 0.7 || radius < 0.58) || (isObstructed))
        {
            for (int x = 0; x < 8; x += 1)
            {
                weightedMap[x] = interestMap[x] - avoidanceMap[x];
                if (weightedMap[x] > 0)
                {
                    resultantVector += (dirArray[x] * (float)weightedMap[x]).normalized * (float)0.20;
                }

            }
            return resultantVector;
        }
        else if (radius >= 0.58 && radius <= 0.7 && !isObstructed)
        {
            Vector3 resultantVectorCircle = new Vector3();
            if (obstructed == false)
            {
                resultantVectorCircle = new Vector3((float)y_diff * -1, (float)x_diff, 0);
                if (Physics2D.Raycast(enemy.position, resultantVectorCircle, 0.08f, LayerMask.GetMask("Obstacles","enemy")))
                {
                    //print("collision detected");
                    resultantVectorCircle = new Vector3((float)y_diff, (float)x_diff * -1, 0);
                    obstructed = true;
                }
            }
            else if (obstructed == true) 
            {
                resultantVectorCircle = new Vector3((float)y_diff, (float)x_diff * -1, 0);
                if (Physics2D.Raycast(enemy.position, resultantVectorCircle, 0.08f, LayerMask.GetMask("Obstacles","enemy")))
                {
                    //print("collision detected");
                    resultantVectorCircle = new Vector3((float)y_diff * -1, (float)x_diff, 0);
                    obstructed = false;
                }
            }
            return resultantVectorCircle;
            
        }
        return Vector3.zero;

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

        enemy_path = Vector3.zero;
        obstructed = false;

        lastKnown = new Vector3();
    }

    private void Update()
    {
        if (animator.GetBool("alive") == true)
        {
            followPlayer();
        }
    }

    private void updatePlayerPos(double x, double y)
    {
        lastKnown.x = (float)x;
        lastKnown.y = (float)y;
    }


    private void followPlayer()
    {
        double x_diff = player.transform.position.x - enemy.transform.position.x;
        double y_diff = player.transform.position.y - enemy.transform.position.y;

        Vector3 dirVector = new Vector3((float)x_diff, (float)y_diff, 0);
        double r = FindRadius(x_diff, y_diff);

        bool isObstructed = Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles"));

        if (r <= 5 && !Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles")))
        {
            updatePlayerPos(player.transform.position.x, player.transform.position.y);
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemySpriteRenderer.flipX = false;
            }
            populateIntMap(x_diff, y_diff, 5, r, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps(x_diff, y_diff, r);
            enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
        }
        else if (r <= 5 && Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles")))
        {
            if ((lastKnown.x  - enemy.transform.position.x)< 0)
            {
                enemySpriteRenderer.flipX = true;
            }
            else
            {
                enemySpriteRenderer.flipX = false;
            }
            double newR = FindRadius((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y));
            populateIntMap((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y), 5, newR, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y), newR);
            enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
        }
    }


}
