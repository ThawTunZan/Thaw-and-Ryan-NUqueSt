using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour, IDataPersistence
{
    public Inventory inventory;
    public PlayerPositionSO startingPosition;
    public GameData data;

    private void Start()
    {
        if (startingPosition.transittedScene)
        { 
            inventory = new Inventory(21);
            inventory = GameManager.instance.inventory;
        }
    }
    private void Update()
    {
      //  if (inventory != null)
     //   {
            GameManager.instance.inventory = inventory;
       // }
      //  else
      //  {
        //    inventory = new Inventory(21);
       // }
    }
    public void LoadData(GameData data)
    {
        inventory = new Inventory(21);
        inventory = data.inventory;
        foreach (Inventory.Slot slot in inventory.slots)
        {
            //slot.AfterDeserialization(slot.iconName);
            slot.icon = Resources.Load<Sprite>("Collectable");
        }
        
    }

    public void SaveData(GameData data)
    {
        data.inventory = inventory;
    }

}
