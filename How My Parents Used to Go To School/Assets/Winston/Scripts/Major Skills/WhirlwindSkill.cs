using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Physics;

[CreateAssetMenu]
public class WhirlwindSkill : MajorSkill
{

    [SerializeField] 
    private float basicDamage = 10f;
    private Hashtable hitList = new Hashtable();
    


    //public WhirlwindOnhit whirl;
    public override void Activate(GameObject player)
    {
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
        Debug.Log(collider.transform.localScale.x * 10);
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
                    c.GetComponent<EnemyHealth>().takeDamage(10);
                                        //  Need a on hit decrese health function here, example: decreaseHealth(Gameobject enemy);
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
