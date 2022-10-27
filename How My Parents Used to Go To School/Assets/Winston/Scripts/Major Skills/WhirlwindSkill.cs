using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Physics;
using UnityEngine.AI;

[CreateAssetMenu]
public class WhirlwindSkill : MajorSkill
{

    [SerializeField] 
    public float basicDamage = 1.0f;
    private Hashtable hitList = new Hashtable();
    GameObject globalPlayer;
    


    //public WhirlwindOnhit whirl;
    public override void Activate(GameObject player)
    {
        globalPlayer = player;
        PlayerController input = player.GetComponent<PlayerController>();
        if (isLearned)
        {
            //whirlwindSpin(player);
            //clearTable();
            launchAttack(player.GetComponent<Collider>());
            hitList.Clear();
        }
        else {
            Debug.Log("Not Learned");
        }
        
    }


    public override void whirlwindSpin(GameObject player) {

        //player.transform.Rotate(0f, 10f, 0f, Space.Self);
        //player.transform.Rotate(0f, 60f, 0f, Space.Self);
        //Debug.Log("WWWWWWWWW");

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
<<<<<<< Updated upstream
                    c.GetComponent<EnemyHealth>().takeDamage(basicDamage);
                                        //  Need a on hit decrese health function here, example: decreaseHealth(Gameobject enemy);
=======
                    c.GetComponent<EnemyHealth>().takeDamage(1);
                    float force = 4;
                    Vector3 vectorForce = Vector3.Normalize(globalPlayer.transform.position - c.transform.position);
                    //c.GetComponent<NavMeshAgent>().isStopped = true;
                    c.GetComponent<Rigidbody>().AddForce(force * -c.transform.forward, ForceMode.VelocityChange);                    //  Need a on hit decrese health function here, example: decreaseHealth(Gameobject enemy);
>>>>>>> Stashed changes
                    count++;
                }
            }
            //Debug.Log("Length" + count );
            //OnDrawGizmos(collider);
            //   Debug.Log(collider.name);
            //Debug.Log(hitList);
        }
    }
    public void clearTable() {
        hitList.Clear();
    }
}
