using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public MajorSkill skill;
    float cooldownTime;
    float activeTime;


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
                    skill.Activate(gameObject);
                    state = SkillState.active;
                    activeTime = skill.activeTime;
                }
                break;
            case SkillState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else {
                    state = SkillState.cooldown;
                    cooldownTime = skill.cooldownTime;
                }
                break;
            case SkillState.cooldown:
                if (activeTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = SkillState.ready;
                }
                break; 
        }


    }

}


