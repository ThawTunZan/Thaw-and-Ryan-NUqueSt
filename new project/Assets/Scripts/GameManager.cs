using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float health;
    public static GameManager instance;
    void Start()
    { 
        instance = this;
        if (instance != null)
        {
            Debug.LogError("Found more than one GameManager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);  //else
    }
}
