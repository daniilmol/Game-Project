using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Physics;

[CreateAssetMenu]
public class WhirlwindSkill : MajorSkill
{

    [SerializeField] float basicDamage = 10f;
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
        player.transform.Rotate(0f, 5f, 0f, Space.Self);
        //Debug.Log("WWWWWWWWW");
    }

    public void IsLearned() {
        isLearned = !isLearned;
        Debug.Log("Learned");
    }


}
