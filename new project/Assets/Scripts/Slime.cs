using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("alive", true);
    }
    public float Health { 
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
            }
        }
        get
        { 
            return _health; 
        }
    }
    public float _health = 3;

    public void OnHit(float damage)
    {
        Debug.Log("Slime hit for " + damage + " damage");
        Health -= damage;
        animator.SetTrigger("Hit");

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SwordAttack"))
        {
            Debug.Log("collision detected witht he slime");
            SwordAttack swordAttack = col.gameObject.GetComponentInParent<SwordAttack>();
            OnHit(swordAttack.swordDamage);
        }
    }
}
