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
    public GameObject hit;
    //public Collider sword;
    public Transform Cube;
    public GameObject playerGameObject;
    public GameObject longrange;
    public GameObject MaleC;
    private GameObject generatedVfx;
    private StatContainer StatContainer;

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
        StatContainer = GetComponent<StatContainer>();
        
    }

    void Update() {
        skill.SetSC(StatContainer);



        switch (state) {
            case SkillState.ready:
                if (Input.GetKeyDown(key))
                {
                    Debug.Log("Clicked");
                    Debug.Log(skill.skillName);
                    //launchAttack(CharacterHitbox);
                    skill.Activate(playerGameObject);
                    state = SkillState.active;
                    activeTime = skill.activeTime;
                    Debug.Log(skill.skillName);
                    //launchAttck(hitBox);
                    //anima.
                    activingSkill = "is" + skill.skillName;
                    anima.takeActivingSkill(activingSkill);


                    Vector3 v3;
                    //v3.x = playerGameObject.transform.position.x;
                    //v3.y = playerGameObject.transform.position.y+2;
                    //v3.z = playerGameObject.transform.position.z;

                    if (skill.skillName == "MortalStrike")
                    {
                        v3.x = MaleC.transform.position.x;
                        v3.y = MaleC.transform.position.y + 2;
                        v3.z = MaleC.transform.position.z;
                        generatedVfx = Instantiate(hit, v3, Quaternion.Euler(0, -90, 0) * MaleC.transform.rotation);
                    }
                    else if (skill.skillName == "Fury_Dual_Wielding") {
                        v3.x = MaleC.transform.position.x;
                        v3.y = MaleC.transform.position.y;
                        v3.z = MaleC.transform.position.z;
                        generatedVfx = Instantiate(hit, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);
                    }
                    else {
                        v3.x = MaleC.transform.position.x;
                        v3.y = MaleC.transform.position.y;
                        v3.z = MaleC.transform.position.z;
                        generatedVfx = Instantiate(hit, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);
                    }
                    
                    Debug.Log(state);
                    //Quaternion.Euler(MaleC.transform.rotation.x, MaleC.transform.rotation.y + 90f, MaleC.transform.rotation.z)
                    //clearTable();
                }
                break;
            case SkillState.active:
                if (activeTime > 0)
                {
                    
                    activeTime -= Time.deltaTime;
                    //skill.whirlwindSpin(gameObject);
                    //skill.MortalStrikeSpin(gameObject);
                   //WhirlOnhit.launchAttack(CharacterHitbox);
                    //OnDrawGizmos(hitBox);

                    //Debug.Log(activeTime);
                }
                else {
                    state = SkillState.cooldown;
                    cooldownTime = skill.cooldownTime / StatContainer.GetAttackSpeed();
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
                    Destroy(generatedVfx);
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
        Vector3 mortal = Cube.position;
        //Ray ray = care.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Vector3 targetVec = hit.point;
        //    targetVec.y = mortal.y;
        //    transform.LookAt(targetVec);
        //    
        //}

        Collider col = longrange.GetComponent<Collider>();
        //mortal.z = mortal.z -1f ;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CharacterHitbox.bounds.center, CharacterHitbox.transform.localScale.x*4); //Whirlwind hitbox
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(CharacterHitbox.bounds.center, CharacterHitbox.transform.localScale.x);
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireCube(col.transform.position, col.transform.localScale);
       // Gizmos.color = Color.green;
       // Gizmos.DrawWireCube(mortal, Cube.transform.localScale);
    }

}


