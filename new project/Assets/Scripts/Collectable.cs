using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //player walks into collectable
    //add collectable to player
    //delete collectable from the screen
    public CollectableType type;
    public Sprite icon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerItems player = collision.GetComponent<PlayerItems>();

        if(player)
        {
            player.inventory.Add(this);
            Destroy(this.gameObject);
        }
    }
}

public enum CollectableType
{
    NONE, TOMATO_SEED
}