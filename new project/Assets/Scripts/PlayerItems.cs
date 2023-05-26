using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour, IDataPersistence
{
    public Inventory inventory;
    public PlayerPositionSO startingPosition;

    private void Start()
    {
        if (startingPosition.transittedScene)
        { 
            inventory = new Inventory(21);
            inventory = GameManager.instance.inventory;
            //inventory = new Inventory(21);
        }
    }
    private void Update()
    {
        GameManager.instance.inventory = inventory;
    }
    public void LoadData(GameData data)
    {
        inventory = new Inventory(21);
        inventory = data.inventory;
        foreach (Inventory.Slot slot in inventory.slots)
        {
            slot.AfterDeserialization();
        }
        
    }

    public void SaveData(GameData data)
    {
        data.inventory = inventory;
    }

}
