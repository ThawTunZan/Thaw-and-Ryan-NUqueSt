using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerItems player = collision.GetComponent<PlayerItems>();

        if(player)
        {
            Item item = GetComponent<Item>();

            if (item != null)
            {
                player.inventory.Add(item);
                Destroy(this.gameObject);
            }
        }
    }
}