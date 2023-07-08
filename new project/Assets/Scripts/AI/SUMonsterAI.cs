using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[SerializeField]
public class SUMonsterAI : EnemyAI
{
    private double distToPlayer;


    private int meleeAttackCooldown = 2000;
    private int throwRocksCooldown = 4000;
    private int slamGroundCooldown = 6000;
    private int chargeCooldown = 6000;

    private bool isMeleeAttackOnCooldown = false;
    private bool isThrowRocksOnCooldown = false;
    private bool isSlamGroundOnCooldown = false;
    private bool isChargeOnCooldown = false;
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
               // SlamGround();
            }
            else if (distToPlayer > 0.7 && distToPlayer <= 1.2)
            {
                Charge();
            }
            else if (distToPlayer > 1.2 && distToPlayer <= 1.6)
            {
               // ThrowRocks();
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public override void followPlayer()
    {
        
        double x_diff = player.transform.position.x - enemy.transform.position.x;
        double y_diff = player.transform.position.y - enemy.transform.position.y;

        Vector3 dirVector = new Vector3((float)x_diff, (float)y_diff, 0);
        double r = FindRadius(x_diff, y_diff);

        bool isObstructed = Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles"));

        if (r <= 4 && !Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles")))
        {
            updatePlayerPos(player.transform.position.x, player.transform.position.y);
            if (gameObject.transform.position.x > player.transform.position.x)
            {
                Vector3 scalePlaceHolder = gameObject.transform.localScale;
                if (scalePlaceHolder.x > 0) { 
                    scalePlaceHolder.x *= -1;
                }
                gameObject.transform.localScale = scalePlaceHolder;
            }
            else
            {
                Vector3 scalePlaceHolder = gameObject.transform.localScale;
                scalePlaceHolder.x = Mathf.Abs(scalePlaceHolder.x);
                gameObject.transform.localScale = scalePlaceHolder;
            }
            populateIntMap(x_diff, y_diff, 5, r, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps(x_diff, y_diff, r);
            if (enemy_path.x != 0 && enemy_path.y != 0)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
        }
        else if (r <= 5 && Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles")))
        {
            if (lastKnown.x == enemy.transform.position.x && lastKnown.y == enemy.transform.position.y)
            {
                return;
            }
            if ((lastKnown.x - enemy.transform.position.x) < 0)
            {
                Vector3 scalePlaceHolder = gameObject.transform.localScale;
                if (scalePlaceHolder.x > 0)
                {
                    scalePlaceHolder.x *= -1;
                }
                gameObject.transform.localScale = scalePlaceHolder;
            }
            else
            {
                Vector3 scalePlaceHolder = gameObject.transform.localScale;
                scalePlaceHolder.x = Mathf.Abs(scalePlaceHolder.x);
                gameObject.transform.localScale = scalePlaceHolder;
            }
            double newR = FindRadius((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y));
            populateIntMap((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y), 5, newR, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps((lastKnown.x - enemy.transform.position.x), (lastKnown.y - enemy.transform.position.y), newR);
            if (enemy_path.x == 0 && enemy_path.y == 0)
            {
                animator.SetBool("isMoving", false);
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
            enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
        }
    }

    private async void MeleeAttack()
    {
        // animation for melee attack
        if (!isMeleeAttackOnCooldown)
        {
            animator.SetTrigger("isAttacking");
            animator.SetBool("isMoving", false);
            isMeleeAttackOnCooldown = true;
            await Task.Delay(meleeAttackCooldown);
            isMeleeAttackOnCooldown = false;
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
        // turn on meleeattack hitbox through animation
    }

    private async void ThrowRocks()
    {
        if (!isThrowRocksOnCooldown)
        {
            // animation for throwing rocks
            // do the instantiate stuff
            isThrowRocksOnCooldown = false;
        }
        else
        {
            // do nothing
        }
        // wait for 2 sec
        await Task.Delay(2000);
    }

    private async void SlamGround()
    {
        // purpose for this is for cooldown
        if (!isSlamGroundOnCooldown)
        {
            // animation for slamming grounds
            // turn on slamground hitbox through animation
            /*
            slamGroundCooldown = true;
            //wait for 2 sec
            await Task.Delay(slamGroundCooldown);
            isSlamGroundOnCooldown = false;
            */
        }
        else
        {
            // do nothing
        }


        // wait for 2 sec
    }

    private async void Charge()
    {
        // animation for charge
        // charge towards player
        if (isC)
        {
            movespeed = 0.3f;
            enemy.MovePosition(enemy.transform.position + vectorTowardsPlayer * movespeed * Time.fixedDeltaTime);
            animator.SetBool("isMoving", true);
            animator.SetTrigger("isCharging");

        }
        else
        {
            animator.SetBool("isMoving", true);
            movespeed = 0.1f;
        }
        canChargeTowardsPlayer = false;
        //wait for 2 sec
        await Task.Delay(2000);
    }
}
