using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public CollectableType type;
        public int count;
        public int maxAllowed;
        public Sprite icon;
        public string iconName;

        public Slot()
        {
            type = CollectableType.NONE;
            count = 0;
            maxAllowed = 32;
            icon = null;
            iconName = null;    
        }

        public bool CanAddItem()
        {
            if(count < maxAllowed)
            {
                return true;
            }
            return false;
        }

        public void AddItem(Collectable item)
        {
            type = item.type;
            icon = item.icon;
            maxAllowed = 32;
            iconName = item.icon.name;
            count++;
        }
        public void AfterDeserialization(string iconNAME)
        {
            string path = "Prefab/" + iconName;
            icon = Resources.Load<Sprite>(iconNAME);
        }

    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Collectable item)
    {
        foreach(Slot slot in slots)
        {
            if(slot.type == item.type && slot.CanAddItem())
            {
                slot.AddItem(item);
                return;
            }
        }

        foreach(Slot slot in slots)
        {
            if(slot.type == CollectableType.NONE)
            {
                slot.AddItem(item);
                return;
            }
        }
    }
}
