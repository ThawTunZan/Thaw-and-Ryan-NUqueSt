using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour, IDataPersistence
{
    public float maxHealth = 100;
    public float health;
    public HealthBar healthBar;
    bool hasCollided;
    public PlayerPositionSO startingPosition;

    private void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        if (startingPosition.transittedScene)
        {
            health = GameManager.instance.health;
            hasCollided = false;                        //used to ensure that u take damage exactly once when you are in aggro range
            healthBar.SetHealth(health);
            healthBar.SetMaxHealth(maxHealth);
        }
        else if (startingPosition.playerDead)
        {
            health = GameManager.instance.health;
            hasCollided = false;
            healthBar.SetHealth(health);
            healthBar.SetMaxHealth(100);
        }
        else
        {
            maxHealth = 100;
            GameManager.instance.health = DataPersistenceManager.instance.gameData.maxHealth;
            hasCollided = false;
            healthBar.SetHealth(health);
            healthBar.SetMaxHealth(maxHealth);
        }
    }
    private void Update()
    {
        GameManager.instance.health = health;
        healthBar.SetHealth(health);
    }

    public void LoadData(GameData data)
    {
        data.maxHealth = 100;
        health = data.maxHealth;
        healthBar.SetMaxHealth(health);
        healthBar.SetHealth(health);
        maxHealth = data.maxHealth;
    }

    public void SaveData(GameData data)
    {
        data.maxHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.CompareTag("Enemy") && !hasCollided && collision.gameObject.GetComponent<Animator>().GetBool("alive"))
        {
            health -= 10;
            healthBar.SetHealth(health);
            hasCollided = true;
            //print(health);
        }
        else if (collision.gameObject.CompareTag("rock"))
        {
            health -= 5;
            healthBar.SetHealth(health);
        }
         else if (collision.gameObject.CompareTag("SUMonsterMelee") && !hasCollided)
        {
             health -= 15;
           healthBar.SetHealth(health);
            hasCollided = true;
         }
         else if (collision.gameObject.CompareTag("SUMonsterRock"))
         {
             health -= 10;
            healthBar.SetHealth(health);
         }
        else if (collision.gameObject.CompareTag("SUMonsterCharge"))
        {
           health -= 15;
           healthBar.SetHealth(health);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            hasCollided = false;
        }
    }


}
