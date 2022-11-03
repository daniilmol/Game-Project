using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Based on code from TheKiwiCoder: https://www.youtube.com/watch?v=_I8HsTfKep8&t=1s
//Standard assets for animation
//Mixamo for Pistol idle animation
//POLYGON starter pack https://assetstore.unity.com/packages/3d/props/polygon-starter-pack-low-poly-3d-art-by-synty-156819?aid=1011ljjCh&utm_campaign=unity_affiliate&utm_medium=affiliate&utm_source=partnerize-linkmaker
public class CharacterLoco : MonoBehaviour
{
    private UserInputs inputActions;
    private InputAction movement;
    public Animator animator;
    Vector2 input;
    bool isRoll = false;
    bool isMoving = false;
    bool isWhirlWind = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputActions = new UserInputs();
        movement = inputActions.Player.Movement;
    }

    // Update is called once per frame
    void Update()
    {
        //get horizontal and vertical inputs, pass to animator to drive movement
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        Rolll();
        moving();

    }

    enum SkillState
    {
        ready,
        active,
        cooldown

    }

    SkillState state = SkillState.ready;

    public KeyCode key;
    float cooldownTime;
    float activeTime;
    void Rolll()
    {
        switch (state)
        {
            case SkillState.ready:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Debug.Log("Clicked");
                    //launchAttack(CharacterHitbox);

                    state = SkillState.active;
                    activeTime = 0.3f;
                    Debug.Log(state);
                    movement.Disable();
                    //clearTable();
                }
                break;
            case SkillState.active:
                if (activeTime > 0)
                {

                    activeTime -= Time.deltaTime;
                    animator.applyRootMotion = true;
                    isRoll = true;
                    animator.SetBool("isRoll", isRoll);
                    movement.Disable();
                    //skill.whirlwindSpin(gameObject);
                    //skill.MortalStrikeSpin(gameObject);
                    //WhirlOnhit.launchAttack(CharacterHitbox);
                    //OnDrawGizmos(hitBox);

                    //Debug.Log(activeTime);
                }
                else
                {
                    state = SkillState.cooldown;
                    cooldownTime = 0.5f;
                    isRoll = false;
                    animator.SetBool("isRoll", isRoll);
                    animator.applyRootMotion = false;
                    movement.Enable();
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
                    isRoll = false;
                    movement.Enable();
                    animator.SetBool("isRoll", isRoll);
                }
                break;
        }


    }

    //private void roll() {
    //    if (Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        // Debug.Log("Preeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeees");
    //        //animator.SetLayerWeight(1, isRoll ? 0 : 1); //lower aiming animation when running. Aimlayer is on layer 1
    //        animator.applyRootMotion = true;
    //        isRoll = !isRoll;
    //        animator.SetBool("isRoll", isRoll);
    //    }

    //    if (Input.GetKeyUp(KeyCode.LeftShift))
    //    {
    //        // Debug.Log("Preeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeees");
    //        //animator.SetLayerWeight(1, isRoll ? 0 : 1); //lower aiming animation when running. Aimlayer is on layer 1
    //        animator.applyRootMotion = false;
    //        isRoll = !isRoll;
    //        animator.SetBool("isRoll", isRoll);
    //    }

    //}
    private void moving() {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S))
        //if (Input.GetKeyUp(KeyCode.W))
        {
            //Debug.Log("Preeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeees");
            //animator.SetLayerWeight(1, isRoll ? 0 : 1); //lower aiming animation when running. Aimlayer is on layer 1
            isMoving = !isMoving;
            animator.SetBool("isMoving", isMoving);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        //if (Input.GetKeyUp(KeyCode.W))
        {
            //Debug.Log("Preeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeees");
            //animator.SetLayerWeight(1, isRoll ? 0 : 1); //lower aiming animation when running. Aimlayer is on layer 1
            isMoving = !isMoving;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public void takeActivingSkill(string skillName) {
        animator.SetBool(skillName, true);
    }

    public void endActivingSkill(string skillName)
    {
        animator.SetBool(skillName, false);
    }






}
