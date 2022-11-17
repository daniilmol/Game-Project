using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Experimental.VFX;

public class ComboHolder : MonoBehaviour
{

    public MajorSkill skill1;
    public MajorSkill skill2;
    public MajorSkill skill3;
    float FirstcooldownTime;
    float SecondcooldownTime;
    float ThirdcooldownTime;
    float activeTime;
    public Collider CharacterHitbox;
    private CharacterLoco anima;
    private string activingSkill;
    public GameObject hit1;
    public GameObject hit2;
    public GameObject hit3;
    public GameObject playerGameObject;
    public GameObject MaleC;
    private GameObject generatedVfx;
    private StatContainer StatContainer;
    public AudioSource skillAudio;


    enum ComboState {
        combo1ready,
        combo1active,
        combo1cooldown,
        combo2ready,
        combo2active,
        combo2cooldown,
        combo3ready,
        combo3active,
        combo3cooldown,
    }

    ComboState cState = ComboState.combo1ready;


    //public KeyCode key = KeyCode.H;
    void Start()
    {
        anima = GetComponent<CharacterLoco>();
        StatContainer = GetComponent<StatContainer>();

    }

    void Update()
    {

        skill1.SetSC(StatContainer);
        skill2.SetSC(StatContainer);
        skill3.SetSC(StatContainer);
        switch (cState) {
            case ComboState.combo1ready:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    
                    FirstcooldownTime = 2f / StatContainer.GetAttackSpeed();
                    skill1.Activate(playerGameObject);
                    Vector3 v3;
                    v3.x = MaleC.transform.position.x;
                    v3.y = MaleC.transform.position.y;
                    v3.z = MaleC.transform.position.z;
                    activingSkill = "is" + skill1.skillName;
                    anima.takeActivingSkill(activingSkill);
                    generatedVfx = Instantiate(hit1, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);

                    activeTime = 0.7f / StatContainer.GetAttackSpeed();
                    //FirstcooldownTime -= Time.deltaTime;
                    
                    Debug.Log("First");
                    cState = ComboState.combo1active;
                }
                    break;
            case ComboState.combo1active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    Destroy(generatedVfx);
                    cState = ComboState.combo2ready;
                    //FirstcooldownTime = skill1.cooldownTime / StatContainer.GetAttackSpeed();
                    anima.endActivingSkill(activingSkill);
                    Debug.Log(cState);

                }
                break;
            //case ComboState.combo1cooldown:
            //    if (FirstcooldownTime > 0)
            //    {
            //        FirstcooldownTime -= Time.deltaTime;
            //        cState = ComboState.combo2ready;
            //    }
            //    else
            //    {
            //        cState = ComboState.combo1ready;
            //        Debug.Log(cState);
            //        Destroy(generatedVfx);
            //    }
            //    break;
            case ComboState.combo2ready:
                if (FirstcooldownTime > 0)
                {
                    FirstcooldownTime -= Time.deltaTime;
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        SecondcooldownTime = 2f / StatContainer.GetAttackSpeed();
                        skill2.Activate(playerGameObject);
                        Vector3 v3;
                        v3.x = MaleC.transform.position.x;
                        v3.y = MaleC.transform.position.y;
                        v3.z = MaleC.transform.position.z;
                        activingSkill = "is" + skill2.skillName;
                        anima.takeActivingSkill(activingSkill);
                        generatedVfx = Instantiate(hit2, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);
                        Debug.Log("Second");
                        activeTime = 0.7f / StatContainer.GetAttackSpeed();

                        cState = ComboState.combo2active;
                    }

                   
                    //if (activeTime > 0)
                    //{
                    //    Debug.Log("2til" + activeTime);
                    //}
                    //else
                    //{

                    //    Destroy(generatedVfx);
                    //    anima.endActivingSkill(activingSkill);
                    //    Debug.Log(cState);

                    //}
                }
                else {// When combo1 is ready roll back to combo1
                    cState = ComboState.combo1ready;
                }
                break;

            case ComboState.combo2active:
                FirstcooldownTime -= Time.deltaTime;
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    Destroy(generatedVfx);
                    cState = ComboState.combo3ready;
                    //SecondcooldownTime = skill2.cooldownTime / StatContainer.GetAttackSpeed();
                    anima.endActivingSkill(activingSkill);
                    Debug.Log(cState);

                }
                break;

            case ComboState.combo3ready:
                if (FirstcooldownTime > 0 && SecondcooldownTime > 0)
                {
                    FirstcooldownTime -= Time.deltaTime;
                    SecondcooldownTime -= Time.deltaTime;
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        ThirdcooldownTime = 2f / StatContainer.GetAttackSpeed();
                        skill3.Activate(playerGameObject);
                        Vector3 v3;
                        v3.x = MaleC.transform.position.x;
                        v3.y = MaleC.transform.position.y;
                        v3.z = MaleC.transform.position.z;
                        activingSkill = "is" + skill3.skillName;
                        anima.takeActivingSkill(activingSkill);
                        generatedVfx = Instantiate(hit3, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);

                        Debug.Log("Third");
                        activeTime = 0.7f / StatContainer.GetAttackSpeed();

                        cState = ComboState.combo3active;


                    }
                }
                else {
                    cState = ComboState.combo1ready;
                }
                break;
            case ComboState.combo3active:
                FirstcooldownTime -= Time.deltaTime;
                SecondcooldownTime -= Time.deltaTime;
                //ThirdcooldownTime -= Time.deltaTime;
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    Destroy(generatedVfx);
                    anima.endActivingSkill(activingSkill);
                    Debug.Log(cState);
                    if (FirstcooldownTime < 0) {
                        cState = ComboState.combo1ready;
                    }
                }
                break;
        }





        //switch (state)
        //{
        //    case SkillState.ready:
        //        // normal combo
        //        if (Input.GetKeyDown(KeyCode.Mouse0) && cState == ComboState.combo1)
        //        {
        //            skill1.Activate(playerGameObject);
        //            Vector3 v3;
        //            v3.x = MaleC.transform.position.x;
        //            v3.y = MaleC.transform.position.y;
        //            v3.z = MaleC.transform.position.z;
        //            generatedVfx = Instantiate(hit1, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);

        //            state = SkillState.active;
        //            activeTime = skill1.activeTime;
        //        }
        //        if (Input.GetKeyDown(KeyCode.Mouse0) && cState == ComboState.combo2)
        //        {
        //            skill2.Activate(playerGameObject);
        //            Vector3 v3;
        //            v3.x = MaleC.transform.position.x;
        //            v3.y = MaleC.transform.position.y;
        //            v3.z = MaleC.transform.position.z;
        //            generatedVfx = Instantiate(hit2, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);

        //            state = SkillState.active;
        //            activeTime = skill2.activeTime;
        //        }
        //        if (Input.GetKeyDown(KeyCode.Mouse0) && cState == ComboState.combo3)
        //        {
        //            skill3.Activate(playerGameObject);
        //            Vector3 v3;
        //            v3.x = MaleC.transform.position.x;
        //            v3.y = MaleC.transform.position.y;
        //            v3.z = MaleC.transform.position.z;
        //            generatedVfx = Instantiate(hit3, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);

        //            state = SkillState.active;
        //            activeTime = skill3.activeTime;
        //        }


        //        break;
        //    case SkillState.active:
        //        if (activeTime > 0)
        //        {

        //            activeTime -= Time.deltaTime;
        //            //skill.whirlwindSpin(gameObject);
        //            //skill.MortalStrikeSpin(gameObject);
        //            //WhirlOnhit.launchAttack(CharacterHitbox);
        //            //OnDrawGizmos(hitBox);

        //            //Debug.Log(activeTime);
        //        }
        //        else
        //        {
        //            state = SkillState.cooldown;
        //            cooldownTime = skill.cooldownTime / StatContainer.GetAttackSpeed();
        //            anima.endActivingSkill(activingSkill);
        //            Debug.Log(state);

        //        }
        //        break;

        //    case SkillState.cooldown:
        //        if (cooldownTime > 0)
        //        {
        //            cooldownTime -= Time.deltaTime;
        //        }
        //        else
        //        {
        //            state = SkillState.ready;
        //            Debug.Log(state);
        //            Destroy(generatedVfx);
        //        }
        //        break;
        //}




        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        //switch (state)
        //{
        //    case SkillState.ready:
        //        // normal combo
        //        if (Input.GetKeyDown(KeyCode.Mouse0) || skill.GetOrder() == 1)
        //        {
        //            skill.Activate(playerGameObject);
        //            skillAudio.Play();
        //            state = SkillState.active;
        //            activeTime = skill.activeTime;
        //            Debug.Log(skill.skillName);
        //            activingSkill = "is" + skill.skillName;
        //            anima.takeActivingSkill(activingSkill);

        //            Vector3 v3;
        //            v3.x = MaleC.transform.position.x;
        //            v3.y = MaleC.transform.position.y;
        //            v3.z = MaleC.transform.position.z;
        //            generatedVfx = Instantiate(hit, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);
        //        }

        //        if (Input.GetKeyDown(KeyCode.Mouse0) || skill.GetOrder() == 2)
        //        {
        //            skill.Activate(playerGameObject);
        //            skillAudio.Play();
        //            state = SkillState.active;
        //            activeTime = skill.activeTime;
        //            Debug.Log(skill.skillName);
        //            activingSkill = "is" + skill.skillName;
        //            anima.takeActivingSkill(activingSkill);

        //            Vector3 v3;
        //            v3.x = MaleC.transform.position.x;
        //            v3.y = MaleC.transform.position.y;
        //            v3.z = MaleC.transform.position.z;
        //            generatedVfx = Instantiate(hit, v3, Quaternion.Euler(0, 0, 0) * MaleC.transform.rotation);
        //        }


        //        break;
        //    case SkillState.active:
        //        if (activeTime > 0)
        //        {
        //            activeTime -= Time.deltaTime;
        //        }
        //        else
        //        {
        //            state = SkillState.cooldown;
        //            cooldownTime = skill.cooldownTime / StatContainer.GetAttackSpeed();
        //            anima.endActivingSkill(activingSkill);
        //            Debug.Log(state);

        //        }
        //        break;

        //    case SkillState.cooldown:
        //        if (cooldownTime > 0)
        //        {
        //            cooldownTime -= Time.deltaTime;
        //        }
        //        else
        //        {
        //            state = SkillState.ready;
        //            Debug.Log(state);
        //            Destroy(generatedVfx);
        //        }
        //        break;
        //}
















    }

    public virtual void launchAttack(Collider collider)
    {

    }



}


