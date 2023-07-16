using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SUMonsterHealth : EnemyHealth
{
    public HealthBar healthBar;
    public override void Start()
    {
        base.Start();
        healthBar = GameObject.Find("SUMonsterHealthBar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(10);
        healthBar.SetHealth(10);
    }

    public override float Health {
        set
        {
            _health = value;
            if (value < 0)
            {
                animator.SetTrigger("Hit");
            }

            if (_health <= 0)
            {
                animator.SetBool("alive", false);
                Invoke(nameof(SlimeDeath), 1f);
            }
        }
        get
        {
            return _health;
        }
    }

    public override void OnHit(float damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health);
        animator.SetTrigger("Hit");
    }

    private void Update()
    {
        healthBar.SetHealth(Health);
    }
}
