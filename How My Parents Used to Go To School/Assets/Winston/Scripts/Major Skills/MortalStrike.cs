using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MortalStrike : MajorSkill
{
    private float gravityScale = 5;

    [SerializeField]
    private float basicDamage = 10f;
    private Hashtable hitList = new Hashtable();
    public override void Activate(GameObject player)
    {
        PlayerController input = player.GetComponent<PlayerController>();
        if (isLearned)
        {
            //MortalStrikeSkill(player);
            launchAttack(player.GetComponent<Collider>());
            hitList.Clear();
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

    public override void MortalStrikeSpin(GameObject player)
    {

        //player.transform.Rotate(0f, 0f, -10f, Space.Self);
        //player.transform.Rotate(0f, 0f, -40f, Space.Self);
        //Debug.Log("WWWWWWWWW");

    }

    public void IsLearned()
    {
        isLearned = !isLearned;
        Debug.Log("Learned");
    }


    public void launchAttack(Collider collider)
    {
        int layerMask = 1 << 7;
        Collider[] cal = Physics.OverlapSphere(collider.bounds.center, collider.transform.localScale.x , layerMask);

        //bool isHit = false;
        int count = 0;
        foreach (Collider c in cal)
        {

            if (!hitList.ContainsKey(c.GetInstanceID()))
            {
                hitList.Add(c.GetInstanceID(), true);
                if (c != collider)
                {
                    Debug.Log("Working");
                    Debug.Log("Hit!!!!!!");
                    Debug.Log(c.name);
                    count++;
                }
            }
            //Debug.Log("Length" + count );
            //OnDrawGizmos(collider);
            //   Debug.Log(collider.name);
            //Debug.Log(hitList);
        }
    }
    public void clearTable()
    {
        hitList.Clear();
    }
}