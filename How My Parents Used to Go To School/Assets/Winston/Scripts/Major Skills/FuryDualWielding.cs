using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FuryDualWielding : MajorSkill
{

    [SerializeField]
    private float basicDamage = 5f;
    private Hashtable hitList = new Hashtable();
    GameObject globalPlayer;
    public Transform m_SpawnTransform;
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
            Debug.Log(input.LongRangeAttackCube);
            launchAttack(input.LongRangeAttackCube.GetComponent<Collider>());
            //Instantiate(input.LongRangeAttackCube, input.LongRangeAttackCube.position, Quaternion.LookRotation);
            hitList.Clear();
        }
        else
        {
            Debug.Log("Not Learned");
        }

    }


    public void IsLearned()
    {
        isLearned = !isLearned;
        Debug.Log("Learned");
    }


    public void launchAttack(Collider collider)
    {
        
        int layerMask = 1 << 7;
        Collider[] cal = Physics.OverlapBox(collider.transform.position, collider.transform.localScale / 2, collider.transform.rotation, layerMask);
        //AttackCube.position = collider.bounds.center;
        //AttackCube = collider.transform;
        //Collider[] cal = Physics.OverlapSphere(collider.bounds.center, collider.transform.localScale.x * 4, layerMask);
        // OnCollisionEnter(collider.collision);

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
        }
    }
    public void clearTable()
    {
        hitList.Clear();
    }


    


    
}
