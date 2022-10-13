using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public MajorSkill skill;
    float cooldownTime;
    float activeTime;
    public Collider hitBox;

    enum SkillState {
        ready,
        active,
        cooldown
    
    }

    SkillState state = SkillState.ready;

    public KeyCode key;


    void FixedUpdate() {
        switch (state) {
            case SkillState.ready:
                if (Input.GetKeyDown(key))
                {
                    state = SkillState.active;
                    activeTime = skill.activeTime;
                    //launchAttck(hitBox);
                    //Debug.Log(state);
                }
                break;
            case SkillState.active:
                if (activeTime > 0)
                {
                    skill.Activate(gameObject);
                    activeTime -= Time.deltaTime;
                    launchAttck(hitBox);
                    //OnDrawGizmos(hitBox);

                    //Debug.Log(activeTime);
                }
                else {
                    state = SkillState.cooldown;
                    cooldownTime = skill.cooldownTime;
                    //Debug.Log(state);
                }
                break;
            case SkillState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = SkillState.ready;
                    Debug.Log(state);
                }
                break; 
        }


    }

    public void launchAttck(Collider collider)
    {
        Collider[] cal = Physics.OverlapSphere(collider.bounds.center, collider.transform.localScale.x);
        Hashtable hitList = new Hashtable();
        bool isHit = false;
        foreach (Collider c in cal)
        {
            if (!hitList.ContainsKey(c.GetInstanceID())) {
                if (c != collider)
                {
                    hitList.Add(c.GetInstanceID(), true);
                    Debug.Log("Hit!!!!!!");
                }
            }

            //OnDrawGizmos(collider);
            //   Debug.Log(collider.name);
            Debug.Log(hitList);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitBox.bounds.center, hitBox.transform.localScale.x);
    }

}


