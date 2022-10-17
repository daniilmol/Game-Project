using System;
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
    private float mouseXPos;
    private float mouseZPos;
    private Vector3 mousePos;
    private Vector3 mousePosition;
    private Vector3 objectPosition;

    // References
    private CharacterController controller;
    private Animator anim;
    public Transform target;
    [SerializeField] private Camera mainCamera;
    private Rigidbody rd;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();

        // if player is not rolling or game menu does not display, player's direction will toward the mouse'direction
        if (isRolling == false && FindObjectOfType<CharectorSceneUIController>().stopFlag != true)
        {
            faceMouseDirection();
        }

        // if not rolling, character can move
        if (isRolling == false)
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
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");


        //moveDirection = new Vector3(x, 0, z);
        moveDirection = new Vector3(0.5f * ((1.73205f * z) - x), 0, 0.5f * (-1.73205f * x - z));

        // tragger animation
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

    private void GetMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mousePos = raycastHit.point;
        }
    }

    private void Roll()
    {
        anim.SetTrigger("Roll");

        //mouseXPos = this.transform.position.x - mousePos.x;
        //mouseZPos = this.transform.position.z - mousePos.z;

        mouseXPos = mousePos.x - this.transform.position.x;
        mouseZPos = mousePos.z - this.transform.position.z;

        //Debug.Log("Mouse: " + mouseXPos + " : " + mouseZPos);
        //Debug.Log("Mouse");
    }

    private void excuteRolling()
    {
        //rd.AddForce(new Vector3(mouseXPos, 0, mouseZPos) * 2);
        //transform.Translate(new Vector3(mouseXPos, 0, mouseZPos) * 2 * Time.deltaTime);
        controller.Move(new Vector3(mouseXPos, 0, mouseZPos) * 2 * Time.deltaTime);
    }

    // rotate the model facing the direction of mouse
    private void faceMouseDirection()
    {
        float angle;
        mousePosition = Input.mousePosition;
        mousePosition.z = 5.23f;
        objectPosition = Camera.main.WorldToScreenPoint(target.position);
        mousePosition.x = mousePosition.x - objectPosition.x;
        mousePosition.y = mousePosition.y - objectPosition.y;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        target.rotation = Quaternion.Euler(new Vector3(0, -angle + 200, 0));
    }
}
