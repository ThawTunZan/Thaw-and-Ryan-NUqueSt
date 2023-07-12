using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockHealth : MonoBehaviour
{
    //private Animator animator;
    //private PlayerQuests player;
    private int caveListIndex;
    public string oreName;

    public void Start()
    {
        //animator = GetComponent<Animator>();
        //animator.SetBool("alive", true);
        //player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        string currScene = SceneManager.GetActiveScene().name;
        if (currScene == "Cave_1")
        {
            caveListIndex = 0;
        }
        else if (currScene == "Cave_1a")
        {
            caveListIndex = 1;
        }
        else if (currScene == "Cave_1b")
        {
            caveListIndex = 2;
        }
        else if (currScene == "Cave_2a")
        {
            caveListIndex = 3;
        }
        else if (currScene == "Cave_3a")
        {
            caveListIndex = 4;
        }
        else if (currScene == "Cave_4a")
        {
            caveListIndex = 5;
        }
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
                int randomDropAmount = UnityEngine.Random.Range(1, 3);
                for (int i = 0; i < randomDropAmount; i++)
                {
                    Instantiate(oreToDrop, gameObject.transform.position, Quaternion.identity);
                }
                PlayerRocks playerRocks = GameObject.Find("Player").GetComponent<PlayerRocks>();
                string rockNameInList = gameObject.transform.parent.gameObject.name;
                int rockIndexInList = playerRocks.listOfRockNames[caveListIndex].FindIndex(x => x == rockNameInList);
                playerRocks.listOfRockStates[caveListIndex][rockIndexInList] = 0;
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
