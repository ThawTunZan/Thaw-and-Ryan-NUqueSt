using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour, IDataPersistence
{
    public Inventory inventory;
    public Inventory toolbar;
    public PlayerPositionSO startingPosition;
    public SpriteRenderer player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (startingPosition.transittedScene)
        {
            inventory = new Inventory(21);
            inventory = GameManager.instance.inventory;
            toolbar = new Inventory(7);
            toolbar = GameManager.instance.toolbar;
            //startingPosition.transittedScene = false;
        }
    }
    private void Update()
    {
        GameManager.instance.inventory = inventory;
        GameManager.instance.toolbar = toolbar;
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        if (player.flipX)
        {
            Instantiate(item, spawnLocation + new Vector2(-0.2f, -0.1f), Quaternion.identity);
        }
        else
        {
            Instantiate(item, spawnLocation + new Vector2(0.2f, -0.1f), Quaternion.identity);
        }
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    public void LoadData(GameData data)
    {
        inventory = new Inventory(21);
        toolbar = new Inventory(7);
        inventory = data.inventory;
        toolbar = data.toolbar;
        foreach (Inventory.Slot slot in inventory.slots)
        {
            //slot.AfterDeserialization(slot.iconName);
            slot.icon = Resources.Load<Sprite>("Collectable");
        }
        foreach (Inventory.Slot slot in toolbar.slots)
        {
            //slot.AfterDeserialization(slot.iconName);
            slot.icon = Resources.Load<Sprite>("Collectable");
        }
    }

    public void SaveData(GameData data)
    {
        data.inventory = inventory;
        data.toolbar = toolbar;
    }

}
