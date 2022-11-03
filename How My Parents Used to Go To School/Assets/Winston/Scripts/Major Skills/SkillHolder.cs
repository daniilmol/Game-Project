using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Experimental.VFX;

public class SkillHolder : MonoBehaviour
{
    public MajorSkill skill;
    float cooldownTime;
    float activeTime;
    public Collider CharacterHitbox;
    private CharacterLoco anima;
    private string activingSkill;
    //public ParticleSystem vfx;
    public VisualEffect vfx;
    public Collider sword;
    enum SkillState {
        ready,
        active,
        cooldown
    
    }

    SkillState state = SkillState.ready;

    public KeyCode key;
    void Start()
    {
        anima = GetComponent<CharacterLoco>();
    }

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
                    Debug.Log(skill.skillName);
                    //launchAttck(hitBox);
                    //anima.
                    activingSkill = "is" + skill.skillName;
                    anima.takeActivingSkill(activingSkill);
                    vfx.SendEvent("OnPlay");
                    Debug.Log(state);
                    
                    //clearTable();
                }
                break;
            case SkillState.active:
                if (activeTime > 0)
                {
                    
                    activeTime -= Time.deltaTime;
                    skill.whirlwindSpin(gameObject);
                    skill.MortalStrikeSpin(gameObject);
                   //WhirlOnhit.launchAttack(CharacterHitbox);
                    //OnDrawGizmos(hitBox);

                    //Debug.Log(activeTime);
                }
                else {
                    state = SkillState.cooldown;
                    cooldownTime = skill.cooldownTime;
                    anima.endActivingSkill(activingSkill);
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
        Vector3 mortal = sword.bounds.center;
        //Ray ray = care.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Vector3 targetVec = hit.point;
        //    targetVec.y = mortal.y;
        //    transform.LookAt(targetVec);
        //    
        //}

            
        //mortal.z = mortal.z -1f ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CharacterHitbox.bounds.center, CharacterHitbox.transform.localScale.x*4); //Whirlwind hitbox
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(CharacterHitbox.bounds.center, CharacterHitbox.transform.localScale.x);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(mortal, new Vector3(CharacterHitbox.transform.localScale.x, CharacterHitbox.transform.localScale.y, CharacterHitbox.transform.localScale.z) );
    }

}


