using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockHealth : MonoBehaviour
{
    //private Animator animator;
    public string oreName;

    private int caveListIndex;
    private string currScene;

    public void Start()
    {
        //animator = GetComponent<Animator>();
        //animator.SetBool("alive", true);
        currScene = SceneManager.GetActiveScene().name;
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
                RockSpawner rockSpawner = GameObject.Find("RockSpawner").GetComponent<RockSpawner>();
                caveListIndex = rockSpawner.listOfRockSceneNames.FindIndex(x => x == currScene);
                string rockNameInList = gameObject.transform.parent.gameObject.name;
                int rockIndexInList = rockSpawner.listOfRockNames[caveListIndex].FindIndex(x => x == rockNameInList);
                rockSpawner.listOfRockStates[caveListIndex][rockIndexInList] = 0;
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
