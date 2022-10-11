using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FuryDualWielding : MajorSkill
{

    public override void Activate(GameObject player)
    {
        PlayerController input = player.GetComponent<PlayerController>();
        // AddSecondWeapon();

        if (isLearned)
        {
            FuryAttack(player);
        }
        else {
            Debug.Log("Not Learned");
        }
       
    }

    public void AddSecondWeapon(GameObject player) {
       // player.
      // player.
    }

    public void FuryAttack(GameObject player)
    {
       // player.transform.Rotate(0f, 20f, 0f, Space.Self);
        Debug.Log("FFFFFFFFFFFFFFFFF");
    }

    public void IsLearned()
    {
        isLearned = !isLearned;
        Debug.Log("Learned");
    }
}
