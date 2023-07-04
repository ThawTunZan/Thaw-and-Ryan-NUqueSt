using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RockHealth : MonoBehaviour
{
    //private Animator animator;
    //private PlayerQuests player;

    public string oreName;

    public void Start()
    {
        //animator = GetComponent<Animator>();
        //animator.SetBool("alive", true);
        //player = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    public float Health
    {
        set
        {
            _health = value;
            if (value < 0)
            {
                //animator.SetTrigger("Hit");
            }
            if (_health <= 0)
            {
                //animator.SetBool("alive", false);
                Item oreToDrop = ItemManager.instance.GetItemByName(oreName);
                int randomDropAmount = Random.Range(1, 3);
                for (int i = 0; i < randomDropAmount; i++)
                {
                    Instantiate(oreToDrop, gameObject.transform.position, Quaternion.identity);
                }
                Destroy(this.gameObject);
            }
        }
        get
        {
            return _health;
        }
    }

    public float _health;

    public void OnHit(float damage)
    {
        Health -= damage;
        //animator.SetTrigger("Hit");
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SwordAttack"))
        {
            SwordAttack swordAttack = col.gameObject.GetComponentInParent<SwordAttack>();
            OnHit(swordAttack.pickaxeDamage);
        }
    }
}
