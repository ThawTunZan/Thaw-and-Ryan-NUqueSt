using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendVillage : MonoBehaviour
{
    private EnemySpawner enemySpawner;

    [SerializeField] private GameObject bossHealthBar;

    // bools below is used to make sure the wave spawns only once
    private bool wave0;
    private bool wave1;
    private bool wave2;
    private bool wave3;

    void Start()
    {
        enemySpawner = EnemySpawner.instance;
    }

    // Spawns a new wave for every hour, regardless of whether the player has killed the enemies
    void Update()
    {
        if (GameManager.instance.hours == 17 && !wave0)
        {
            SpawnWave0();
            wave0 = true;
        }
        if (GameManager.instance.hours == 18 && !wave1)
        {
            SpawnWave1();
            wave1 = true;
        }
        else if (GameManager.instance.hours == 19 && !wave2)
        {
            SpawnWave2();
            wave2 = true;
        }
        else if (GameManager.instance.hours == 20 && !wave3)
        {
            bossHealthBar.SetActive(true);
            Invoke(nameof(SpawnWave3), 0.1f);
            wave3 = true;
        }
    }

    private void SpawnWave0()
    {
        Instantiate(enemySpawner.GetEnemyByName("Slime"), new Vector2(-0.009534121f, -0.7193651f), Quaternion.identity);
    }

    private void SpawnWave1()
    {
        Instantiate(enemySpawner.GetEnemyByName("Goblin"), new Vector2(-0.009534121f, -0.7193651f), Quaternion.identity);
    }

    private void SpawnWave2()
    {
        Instantiate(enemySpawner.GetEnemyByName("Skeleton"), new Vector2(-0.009534121f, -0.7193651f), Quaternion.identity);
    }

    private void SpawnWave3()
    {
        Instantiate(enemySpawner.GetEnemyByName("01"), new Vector2(-0.009534121f, -0.7193651f), Quaternion.identity);
    }
}
