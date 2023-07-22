using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll_UI : MonoBehaviour
{
    [SerializeField] private GameObject scrollPanel;
    [SerializeField] private GameObject noScrollPanel;
    [SerializeField] private GameObject hasScrollPanel;
    [SerializeField] private GameObject insertScrollPanel;

    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    private int invIndex = -1;
    private int scrollIndex = -1;

    [HideInInspector] public int stoneIndex = -1;
    [HideInInspector] public bool hasInserted = false;

    void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (scrollPanel.activeSelf && !insertScrollPanel.activeSelf)
        {
            for (int i = 0; i < 21; i++)
            {
                if (playerItems.inventory.slots[i].itemName == "Scroll")
                {
                    invIndex = 0;
                    scrollIndex = i;
                    hasScrollPanel.SetActive(true);
                    break;
                }
            }
            for (int i = 0; i < 7; i++)
            {
                if (playerItems.toolbar.slots[i].itemName == "Scroll")
                {
                    invIndex = 1;
                    scrollIndex = i;
                    hasScrollPanel.SetActive(true);
                    break;
                }
            }
            if (invIndex == -1 || scrollIndex == -1)
            {
                noScrollPanel.SetActive(true);
            }
        }
    }

    public void YesButton()
    {
        if (invIndex == 0)
        {
            playerItems.inventory.Remove(scrollIndex, 1);
        }
        else if (invIndex == 1)
        {
            playerItems.toolbar.Remove(scrollIndex, 1);
        }
        invIndex = -1;
        scrollIndex = -1;
        GameObject stoneScroll = GameObject.Find("StoneScroll" + stoneIndex);
        stoneScroll.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Quest/StoneHasScroll");
        hasInserted = true;
        stoneScroll.transform.Find("VisualCue").gameObject.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerQuests>().questScrollInserted[stoneIndex] = 1;
        noScrollPanel.SetActive(false);
        hasScrollPanel.SetActive(false);
        insertScrollPanel.SetActive(true);
    }

    public void CloseUI()
    {
        if (hasInserted && GameObject.Find("StoneScroll" + stoneIndex).transform.Find("Trigger") != null)
        {
            GameObject.Find("StoneScroll" + stoneIndex).transform.Find("Trigger").gameObject.SetActive(false);
            stoneIndex = -1;
            hasInserted = false;
        }
        scrollPanel.SetActive(false);
        noScrollPanel.SetActive(false);
        hasScrollPanel.SetActive(false);
        insertScrollPanel.SetActive(false);
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;
    }
}
