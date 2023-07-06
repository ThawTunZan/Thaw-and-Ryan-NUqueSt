using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SUMonsterAI : EnemyAI
{
    private double distToPlayer;

    public bool canChargeTowardsPlayer;
    public bool canSmashGround;
    public bool canThrow;

    private Vector3 vectorTowardsPlayer;
    public override void Update()
    {
        
        if (animator.GetBool("alive") == true)
        {
            vectorTowardsPlayer = new Vector3(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y, 0);
            followPlayer();
            distToPlayer = FindRadius(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y);
            if (distToPlayer <= 0.3)
            {
                MeleeAttack();
            }
            else if (distToPlayer > 0.3 && distToPlayer <= 0.7)
            {
               canSmashGround = true;
                SlamGround();
            }
            else if (distToPlayer > 0.7 && distToPlayer <= 1.2)
            {
                canChargeTowardsPlayer = true;
                Charge();
            }
            else if (distToPlayer > 1.2 && distToPlayer <= 1.6)
            {
                canThrow = true;
                ThrowRocks();
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    
    private void MeleeAttack()
    {
        // animation for melee attack
        // turn on meleeattack hitbox through animation

        // wait for 1 sec

    }

    private async void ThrowRocks()
    {
        if (canThrow)
        {
            // animation for throwing rocks
            // do the instantiate stuff
        }
        else
        {
            // do nothing
        }
        canThrow = false;
        // wait for 2 sec
        await Task.Delay(2000);
    }

    private async void SlamGround()
    {
        // purpose for this is for cooldown
        if (canSmashGround)
        {
            // animation for slamming grounds
            // turn on slamground hitbox through animation
        }
        else
        {
            // do nothing
        }
        canSmashGround = false;
        //wait for 2 sec
        await Task.Delay(2000);

        // wait for 2 sec
    }

    private async void Charge()
    {
        // animation for charge
        // charge towards player
        if (canChargeTowardsPlayer)
        {
            movespeed = 0.2f;
            enemy.MovePosition(vectorTowardsPlayer);
        }
        else
        {
            movespeed = 0.1f;
        }
        canChargeTowardsPlayer = false;
        //wait for 2 sec
        await Task.Delay(2000);
    }
}
