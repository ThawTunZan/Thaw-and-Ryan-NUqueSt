using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedWeaponshopTrigger : MonoBehaviour
{
    public WeaponShopTutorial_UI tutorialUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameManager.instance.tutorialProgress == 1)
            {
                tutorialUI.tutorialProgress = 2;
            }
            Destroy(this.gameObject);
        }
    }
}