using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WhirlwindSkill : MajorSkill
{

    public override void Activate(GameObject player)
    {
        PlayerController input = player.GetComponent<PlayerController>();
        if (isLearned)
        {
            whirlwindSpin(player);
        }
        else {
            Debug.Log("Not Learned");
        }
        
    }


    public void whirlwindSpin(GameObject player) {
        player.transform.Rotate(0f, 20f, 0f, Space.Self);
        Debug.Log("WWWWWWWWW");
    }

    public void IsLearned() {
        isLearned = !isLearned;
        Debug.Log("Learned");
    }
}
