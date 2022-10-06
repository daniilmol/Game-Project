using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WhirlwindSkill : MajorSkill
{

    public override void Activate(GameObject player)
    {
        PlayerController input = player.GetComponent<PlayerController>();
        whirlwindSpin(player);
    }


    public void whirlwindSpin(GameObject player) {
        player.transform.Rotate(0f, 1000f, 0f, Space.Self);
        Debug.Log("WWWWWWWWW");
    }
}
