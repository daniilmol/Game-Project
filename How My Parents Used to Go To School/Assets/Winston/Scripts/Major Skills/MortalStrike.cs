using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MortalStrike : MajorSkill
{

    [SerializeField]
    private float basicDamage = 10f;
    private Hashtable hitList = new Hashtable();
    GameObject globalPlayer;
    public GameObject sword;
    // public Transform AttackCube;

    public override void Activate(GameObject player)
    {
        globalPlayer = player;
        PlayerController input = player.GetComponent<PlayerController>();
        if (isLearned)
        {
            //AttackCube = player.getCube();
            //MortalStrikeSkill(player);
            //player.transform.Find("childname");
            //launchAttack(player.transform.Find("Player/Male C/Character/Character Pelvis/Character Spine/Character Spine1/Character Spine2/Character R Clavicle/Character R UpperArm/Character R Forearm/Character R Hand/TestSword").transform.GetComponent<Collider>());
            //get the sword collider
            //Debug.Log(input.getCube());
            Debug.Log(input.AttackCube);
            Debug.Log(sword);
            launchAttack(input.AttackCube.GetComponent<Collider>());
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
        //Rigidbody rb = player.GetComponent<Rigidbody>();
        //rb.AddForce(0f, 100f, 0f);
        //rb.AddForce(Physics.gravity * 3);
        //Debug.Log(Physics.gravity * 3);
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
        Collider[] cal = Physics.OverlapBox(collider.bounds.center, collider.transform.localScale, Quaternion.identity, layerMask);
        //AttackCube.position = collider.bounds.center;
        //AttackCube = collider.transform;
        //Collider[] cal = Physics.OverlapSphere(collider.bounds.center, collider.transform.localScale.x * 4, layerMask);
        Debug.Log("youyou");
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
                    c.GetComponent<EnemyHealth>().takeDamage(basicDamage);
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