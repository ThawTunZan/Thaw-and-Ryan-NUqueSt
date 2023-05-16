using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public HealthBar healthBar;
    bool hasCollided;

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        health = maxHealth;
        GameManager.instance.health = health;
        hasCollided = false;
        healthBar.SetMaxHealth(health);
    }
    private void Update()
    {
        GameManager.instance.health = health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !hasCollided && collision.gameObject.GetComponent<Animator>().GetBool("alive"))
        {
            health -= 10;
            healthBar.SetHealth(health);
            hasCollided=true;
            //print(health);
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
