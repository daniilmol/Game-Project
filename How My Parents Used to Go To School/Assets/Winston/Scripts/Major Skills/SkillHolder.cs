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
                   
                    state = SkillState.active;
                    activeTime = skill.activeTime;
                    Debug.Log(state);
                }
                break;
            case SkillState.active:
                if (activeTime > 0)
                {
                    skill.Activate(gameObject);
                    activeTime -= Time.deltaTime;
                    Debug.Log(activeTime);
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

}


