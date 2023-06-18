using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHealth : EnemyHealth
{
    public override void SlimeDeath()
    {
        for (int i = 0; i < 5; i++)
        {
            //if there is an active quest in the slot
            if (player.questList.questSlots[i].count == 1)
            {
                print("slime getting destroyed");
                player.questList.questSlots[i].slimesRequired--;
            }
        }
        Destroy(this.gameObject);
    }
}
