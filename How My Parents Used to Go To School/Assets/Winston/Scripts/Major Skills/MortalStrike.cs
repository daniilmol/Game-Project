using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MortalStrike : MajorSkill
{
    private float gravityScale = 5;
    public override void Activate(GameObject player)
    {
        PlayerController input = player.GetComponent<PlayerController>();
        if (isLearned)
        {
            MortalStrikeSkill(player);
        }
        else {
            Debug.Log("Not Learned");
        }
        
    }


    public void MortalStrikeSkill(GameObject player)
    {
        //Mortal Strike!!!!
        //GameObject fixedd = player.GetComponentInChildren<FixedJoint>();
        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.AddForce(0f, 100f, 0f);
        rb.AddForce(Physics.gravity * 3);
        Debug.Log(Physics.gravity * 3);
        Debug.Log("MMMMMMMMMMMM");
    }

    public void IsLearned()
    {
        isLearned = !isLearned;
        Debug.Log("Learned");
    }
}