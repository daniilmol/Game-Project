using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    c.GetComponent<EnemyHealth>().takeDamage(1);

                }
            }

            //OnDrawGizmos(collider);
            //   Debug.Log(collider.name);
            Debug.Log(hitList);
        }
    }
}
