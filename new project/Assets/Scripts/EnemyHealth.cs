using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestList;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;

    public PlayerQuests player;

    public bool isBeingHit;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("alive", true);
        
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        isBeingHit = false;
    }

    public virtual float Health { 
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
                Invoke(nameof(SlimeDeath), 2f);
            }
        }
        get
        { 
            return _health; 
        }
    }
    public float _health = 3;

    public virtual void OnHit(float damage)
    {
        Health -= damage;
        animator.SetTrigger("Hit");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SwordAttack") && !isBeingHit)
        {
            SwordAttack swordAttack = col.gameObject.GetComponentInParent<SwordAttack>();
            OnHit(swordAttack.swordDamage);
            isBeingHit = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isBeingHit = false;
    }

    public virtual void SlimeDeath()
    {
        Destroy(this.gameObject);
    }
}
