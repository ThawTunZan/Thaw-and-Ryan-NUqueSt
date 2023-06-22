using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestList;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;

    public PlayerQuests player;
    //public DialogueManager dialogueManager;

    public void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("alive", true);
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();

        //dialogueManager = GameObject.Find("DialogueManager").GetComponent <DialogueManager>();
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
                /*
                for (int i = 0; i < 5; i++)
                {
                    //if there is an active quest in the slot
                    if (player.questList.questSlots[i].count == 1)
                    {
                        player.questList.questSlots[i].slimesRequired--;
                    }
                }
                */
                Invoke(nameof(SlimeDeath), 1f);
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
        Health -= damage;
        animator.SetTrigger("Hit");
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SwordAttack"))
        {
            SwordAttack swordAttack = col.gameObject.GetComponentInParent<SwordAttack>();
            OnHit(swordAttack.swordDamage);
        }
    }

    public virtual void SlimeDeath()
    {

        Destroy(this.gameObject);
    }
}
