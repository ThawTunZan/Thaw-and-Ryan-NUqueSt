using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerItems playerItems = collision.GetComponent<PlayerItems>();

        if(playerItems)
        {
            Item item = GetComponent<Item>();

            if (item != null)
            {
                playerItems.inventory.Add(item);
                Destroy(this.gameObject);
            }
        }
    }
}