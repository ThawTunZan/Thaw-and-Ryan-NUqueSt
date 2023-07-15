using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeHealth : EnemyHealth
{
    public override void SlimeDeath()
    {
        for (int i = 0; i < 5; i++)
        {
            //if there is an active quest in the slot
            if (player.questList.questSlots[i].count == 1)
            {
                string currScene = SceneManager.GetActiveScene().name;
                if (currScene == "Village_WeaponShop" && player.questList.questSlots[i].questName == "MA1511")
                {
                    player.questList.questSlots[i].slimesRequired--;
                }
                else if (player.questList.questSlots[i].questName != "MA1511")
                {
                    player.questList.questSlots[i].slimesRequired--;
                }
            }
        }
        Destroy(this.gameObject);
    }
}
