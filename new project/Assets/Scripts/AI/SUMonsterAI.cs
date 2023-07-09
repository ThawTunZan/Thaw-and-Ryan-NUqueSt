using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[SerializeField]
public class SUMonsterAI : EnemyAI
{
    private double distToPlayer;


    private int throwRocksCooldown = 4000;
    private int slamGroundCooldown = 6000;
    private int chargeCooldown = 8000;

    public bool isThrowRocksOnCooldown = false;
    public bool isSlamGroundOnCooldown = false;
    public bool isChargeOnCooldown = false;
    public Vector3 vectorTowardsPlayer;
    public bool isCharging;

    private float chargeCD;


    public override void Update()
    {
        
        if (animator.GetBool("alive") == true)
        {
            vectorTowardsPlayer = new Vector3(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y, 0);
            distToPlayer = FindRadius(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y);
            if (!isCharging)
            {
                followPlayer();
             }
            chargeCD += Time.deltaTime;
            if (distToPlayer <= 0.1)
            {
                MeleeAttack();
            }
            else if (distToPlayer > 0.3 && distToPlayer <= 0.7)
            {
               // SlamGround();
            }
            else if (distToPlayer > 0.7 && distToPlayer <= 1.3 && !isCharging && !isChargeOnCooldown)
            {
                Charge(player.transform.position);
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
            //to flip sprite
            if (gameObject.transform.position.x > player.transform.position.x)
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
            populateIntMap(x_diff, y_diff, 5, r, isObstructed);
            populateAvoidMap();

            enemy_path = weighTheMaps(x_diff, y_diff, r);
            if (distToPlayer > 0.1 && !isCharging)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
                MeleeAttack();
            }
            enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);

        }
        else if (r <= 5 && Physics2D.Raycast(transform.position, dirVector, (float)r, LayerMask.GetMask("Obstacles")))
        {
            if (lastKnown.x == enemy.transform.position.x && lastKnown.y == enemy.transform.position.y)
            {
                return;
            }
            // to flip sprite
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
            if (distToPlayer > 0.1 && !isCharging)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
                MeleeAttack();
            }
            enemy.MovePosition(enemy.transform.position + enemy_path * movespeed * Time.fixedDeltaTime);
        }
    }

    private void MeleeAttack()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("isAttacking");
        // turn on meleeattack hitbox through animation
    }

    private void ThrowRocks()
    {
    }

    private void SlamGround()
    {
    }

    private async void Charge(Vector3 targetDir)
    {
        print("How many time is this getting called?");
        Vector3 chargeDirPlaceHolder = new Vector3(player.transform.position.x - enemy.transform.position.x, player.transform.position.y - enemy.transform.position.y, 0);
        isChargeOnCooldown = true;
        // = true;
        animator.SetBool("isMoving", false);
        animator.SetTrigger("isCharging");
        Vector3 chargeDir = chargeDirPlaceHolder.normalized * 0.2f;
        enemy.MovePosition(enemy.transform.position + chargeDir * movespeed * Time.fixedDeltaTime);
        //isCharging = false;
        await Task.Delay(chargeCooldown);
        isChargeOnCooldown = false;
    }
}
