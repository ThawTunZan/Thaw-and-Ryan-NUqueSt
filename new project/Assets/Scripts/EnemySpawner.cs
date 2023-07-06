using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    private Dictionary<string, GameObject> nameToEnemyDict = new Dictionary<string, GameObject>();

    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;

        foreach (GameObject enemy in enemies)
        {
            AddItem(enemy);
        }
    }

    private void AddItem(GameObject enemy)
    {
        if (!nameToEnemyDict.ContainsKey(enemy.name))
        {
            nameToEnemyDict.Add(enemy.name, enemy);
        }
    }

    public GameObject GetEnemyByName(string key)
    {
        if (nameToEnemyDict.ContainsKey(key))
        {
            return nameToEnemyDict[key];
        }
        return null;
    }
}
