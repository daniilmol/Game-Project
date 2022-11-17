using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Physics;
using UnityEngine.AI;

//[CreateAssetMenu]
public class WhirlwindSkill : MajorSkill
{

    [SerializeField]
    public StatContainer playerStats;
    //public float basicDamage;
    private Hashtable hitList = new Hashtable();
    GameObject globalPlayer;
    

    //public void Update() { 
    //    basicDamage =  playerStats.GetDamage() * 2;
    //    Debug.Log("Get" + playerStats.GetDamage());
    //    Debug.Log("Damage" + basicDamage);
    //}

    //public WhirlwindOnhit whirl;
    public override void Activate(GameObject player)
    {
        SetOrder(1);
        globalPlayer = player;
        Transform skill = player.transform.GetChild(0);
        PlayerController input = player.GetComponent<PlayerController>();
        if (isLearned)
        {
            //whirlwindSpin(player);
            //clearTable();
            launchAttack(skill.GetComponent<Collider>());
            hitList.Clear();
        }
        else {
            Debug.Log("Not Learned");
        }
        
    }


    public void IsLearned() {
        isLearned = !isLearned;
        Debug.Log("Learned");
    }

    public void launchAttack(Collider collider)
    {
        int layerMask = 1 << 7;
        Collider[] cal = Physics.OverlapSphere(collider.bounds.center, collider.transform.localScale.x*4, layerMask);
        Debug.Log(collider.transform.localScale.x * 4);
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
                    Debug.Log("PUSHING ENEMY BACK");
                    Debug.Log(c.name);
                    c.GetComponent<EnemyHealth>().takeDamage(StatContainer.GetDamage());
                                        //  Need a on hit decrese health function here, example: decreaseHealth(Gameobject enemy);
                    float force = 6;
                    Vector3 vectorForce = Vector3.Normalize(globalPlayer.transform.position - c.transform.position);
                    //c.GetComponent<NavMeshAgent>().isStopped = true;
                    c.GetComponent<Rigidbody>().AddForce(force * -c.transform.forward, ForceMode.Impulse);                    //  Need a on hit decrese health function here, example: decreaseHealth(Gameobject enemy);
                    count++;
                }
            }
            //Debug.Log("Length" + count );
            //OnDrawGizmos(collider);
            //   Debug.Log(collider.name);
            //Debug.Log(hitList);
        }
    }
    void OnCollisionEnter() { 
    
    }
    public void clearTable() {
        hitList.Clear();
    }

    public int getOrder() {
        order = 1;
        return order;
    }
}
