using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhirlwindOnhit : SkillHolder
{
    // Start is called before the first frame update
    public override void launchAttack(Collider collider) {
        Debug.Log("Working");
        Collider[] cal = Physics.OverlapSphere(collider.bounds.center, collider.transform.localScale.x);
        Hashtable hitList = new Hashtable();
       // bool isHit = false;
        foreach (Collider c in cal)
        {
            if (!hitList.ContainsKey(c.GetInstanceID()))
            {
                if (c != collider)
                {
                    hitList.Add(c.GetInstanceID(), true);
                    Debug.Log("Hit!!!!!!");
                    Debug.Log("PUSHING PLAYER BACK IN ON HIT");
                    c.GetComponent<EnemyHealth>().takeDamage(1);
                    float force = 50;
                    Vector3 vectorForce = Vector3.Normalize(gameObject.transform.position - c.transform.position);
                    //c.GetComponent<NavMeshAgent>().isStopped = true;
                    c.GetComponent<Rigidbody>().AddForce(force * -transform.forward, ForceMode.Impulse);
                }
            }

            //OnDrawGizmos(collider);
            //   Debug.Log(collider.name);
            Debug.Log(hitList);
        }
    }
}
