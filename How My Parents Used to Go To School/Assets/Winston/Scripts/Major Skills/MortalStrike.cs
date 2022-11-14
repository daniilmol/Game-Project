using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu]
public class MortalStrike : MajorSkill
{

    [SerializeField]
    public StatContainer playerStats;
    //public float basicDamage;
    private Hashtable hitList = new Hashtable();
    GameObject globalPlayer;
    
    // public Transform AttackCube;
    //public void Update()
    //{
    //    basicDamage = playerStats.GetDamage() * 5f;
    //}



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
            launchAttack(input.AttackCube.GetComponent<Collider>());
            //vfx.SendEvent("OnPlay");
            hitList.Clear();
        }
        else {
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
        Collider[] cal = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents * 2, Quaternion.identity, layerMask);
        //AttackCube.position = collider.bounds.center;
        //AttackCube = collider.transform;
        //Collider[] cal = Physics.OverlapSphere(collider.bounds.center, collider.transform.localScale.x * 4, layerMask);
        //Debug.Log("youyou");
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
                    c.GetComponent<EnemyHealth>().takeDamage(StatContainer.GetDamage() * 2);


                    float force = 6;
                    Vector3 vectorForce = Vector3.Normalize(globalPlayer.transform.position - c.transform.position);
                    //c.GetComponent<NavMeshAgent>().isStopped = true;
                    c.GetComponent<Rigidbody>().AddForce(force * -c.transform.forward, ForceMode.Impulse);

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