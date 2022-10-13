using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public MajorSkill skill;
    float cooldownTime;
    float activeTime;
    public Collider CharacterHitbox;
    private WhirlwindOnhit WhirlOnhit;
    enum SkillState {
        ready,
        active,
        cooldown
    
    }

    SkillState state = SkillState.ready;

    public KeyCode key;


    void Update() {
        switch (state) {
            case SkillState.ready:
                if (Input.GetKeyDown(key))
                {
                    Debug.Log("Clicked");
                    //launchAttack(CharacterHitbox);
                    skill.Activate(gameObject);
                    state = SkillState.active;
                    activeTime = skill.activeTime;
                    //launchAttck(hitBox);
                    Debug.Log(state);

                    //clearTable();
                }
                break;
            case SkillState.active:
                if (activeTime > 0)
                {
                    
                    activeTime -= Time.deltaTime;
                    skill.whirlwindSpin(gameObject);
                   //WhirlOnhit.launchAttack(CharacterHitbox);
                    //OnDrawGizmos(hitBox);

                    //Debug.Log(activeTime);
                }
                else {
                    state = SkillState.cooldown;
                    cooldownTime = skill.cooldownTime;
                    Debug.Log(state);
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

    public virtual void launchAttack(Collider collider)
    {

    }

    public virtual void clearTable() { 
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CharacterHitbox.bounds.center, CharacterHitbox.transform.localScale.x*4); //Whirlwind hitbox
    }

}


