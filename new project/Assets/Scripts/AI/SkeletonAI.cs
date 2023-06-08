using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : EnemyAI
{
    public override Vector3 weighTheMaps(double x_diff, double y_diff, double radius)
    {
        double xToPlayer = player.transform.position.x - enemy.transform.position.x;
        double yToPlayer = player.transform.position.y - enemy.transform.position.y;
        double r = FindRadius(xToPlayer, yToPlayer);
        Vector3 dirToPlayer = new Vector3((float)xToPlayer, (float)yToPlayer, 0);
        bool isObstructed = Physics2D.Raycast(transform.position, dirToPlayer, (float)r, LayerMask.GetMask("Obstacles"));

        Vector3 resultantVector = new Vector3();

        if ((radius > 0.7 || radius < 0.58) || isObstructed)
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
        

        // isObstructed is to see if there is a clear line of sight between enemy and player. Returns true if there is no clear line of sight
        if (radius >= 0.58 && radius <= 0.7 && !isObstructed)
        {
            Vector3 resultantVectorCircle = new Vector3();
            // obstructed is to see if enemy detects a collision while circling
            if (obstructed == false)
            {
                resultantVectorCircle = new Vector3((float)y_diff * -1, (float)x_diff, 0);
                if (Physics2D.Raycast(enemy.position, resultantVectorCircle, 0.08f, LayerMask.GetMask("Obstacles", "enemy")))
                {
                    //print("collision detected");
                    resultantVectorCircle = new Vector3((float)y_diff, (float)x_diff * -1, 0);
                    obstructed = true;
                }
            }
            else if (obstructed == true)
            {
                resultantVectorCircle = new Vector3((float)y_diff, (float)x_diff * -1, 0);
                if (Physics2D.Raycast(enemy.position, resultantVectorCircle, 0.08f, LayerMask.GetMask("Obstacles", "enemy")))
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
}