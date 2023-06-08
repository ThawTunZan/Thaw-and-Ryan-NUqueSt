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

    [SerializeField] private GameObject highlight;

    /*
     * SetItem updates the sprite and quantity text in the UI by matching it to the corresponding slot in the player's inventory
     */
    public void SetItem(Inventory.Slot slot)
    {
        if(slot != null)
        {
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
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    public void SetHighlight(bool isOn)
    {
        highlight.SetActive(isOn);
    }
}
