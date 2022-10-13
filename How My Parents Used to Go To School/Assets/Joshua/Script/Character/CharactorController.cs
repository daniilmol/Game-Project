using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    // Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSPeed;

    private Vector3 moveDirection;
    private bool isRolling = false;

    // References
    private CharacterController controller;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Debug.Log("Game");
    }

    // Update is called once per frame
    void Update()
    {
        // if not rolling, character can move
        if(isRolling == false)
        {
            Move();
        }

        // left click to attack
        if(Input.GetKeyDown(KeyCode.Mouse0) && isRolling == false)
        {
            StartCoroutine(Attack());
        }

        // right click to roll
        if (Input.GetKeyDown(KeyCode.Mouse1) && isRolling == false)
        {
            Roll();
        }

        // if character rolling, move character forword
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BaseLayer.Roll"))
        {
            excuteRolling();

            isRolling = true;
        }
        else
        {
            isRolling = false;
        }
    }

    public void Move()
    {
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            // Walk
            Walk();
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            // Run
            Run();
        }
        else if (moveDirection == Vector3.zero)
        {
            // Idle
            Idel();
        }

        moveDirection *= moveSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        /*AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (info.normalizedTime >= 1.0f && info.IsName("Rolling"))
        {

            Debug.Log("Roll stop");

        }*/
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSPeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Idel()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }

    // run rolling animation
    private void Roll()
    {
        anim.SetTrigger("Roll");

        // controller.Move(new Vector3(0, 0, 3.63f));
    }

    // move character when tragger the rolling animation
    private void excuteRolling()
    {
        controller.Move(Camera.main.transform.forward * 10f * Time.deltaTime);
    }
}
