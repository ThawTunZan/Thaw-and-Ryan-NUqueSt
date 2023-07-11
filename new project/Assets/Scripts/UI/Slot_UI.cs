using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Slot_UI : MonoBehaviour
{
    public string inventoryName;
    public int slotID;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    public string itemName;
    public string itemDesc;
    public int itemBuyCost;
    public int itemSellCost;

    [SerializeField] private GameObject highlight;

    /*
     * SetItem updates the sprite and quantity text in the UI by matching it to the corresponding slot in the player's inventory
     */
    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
            itemName = slot.itemName; 
            itemDesc = slot.itemDesc;
            itemBuyCost = slot.itemBuyCost;
            itemSellCost = slot.itemSellCost;
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            if (slot.maxAllowed == 1)
            {
                quantityText.text = "";
            }
            else
            {
                quantityText.text = slot.count.ToString();
            }
        }
    }

    public void SetEmpty()
    {
        itemName = null;
        itemDesc = null;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }
}
