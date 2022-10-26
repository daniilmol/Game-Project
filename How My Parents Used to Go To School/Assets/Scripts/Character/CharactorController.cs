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
    private Vector3 mousePos;

    private bool enableRotationFlag = true;

    // References
    private CharacterController controller;
    private Animator anim;
    public Transform target;
    [SerializeField] private Camera mainCamera;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //anim = GetComponentInChildren<Animator>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();

        // if player is not rolling or game menu does not display, player's direction will toward the mouse'direction
        if (isRolling == false && FindObjectOfType<CharectorSceneUIController>().stopFlag != true && enableRotationFlag == true)
        {
            faceMouseDirection();
        }

        // if not rolling, character can move
        if (isRolling == false)
        {
            Move();
        }

        // left click to attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && isRolling == false)
        {
            if (!this.anim.GetCurrentAnimatorStateInfo(1).IsName("AttackLayer.Attack"))
            {
                StartCoroutine(Attack());
            }
        }

        // right click to roll
        if (Input.GetKeyDown(KeyCode.LeftShift) && isRolling == false)
        {
            Roll();
        }

        // if character rolling, move character forword
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("BaseLayer.Roll"))
        {
            isRolling = true;
            enableRotationFlag = true;
        }
        else
        {
            isRolling = false;
        }
    }

    // move the player
    public void Move()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(x, 0, z);
        moveDirection = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * moveDirection;

        anim.SetFloat("InputX", moveDirection.x);
        anim.SetFloat("InputY", moveDirection.z);

        moveDirection *= moveSpeed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    // trigger attack animation
    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("AttackLayer"), 1);

        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("AttackLayer"), 0);
    }

    // acquire mouse position
    private void GetMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mousePos = raycastHit.point;
        }
    }

    // rotate player's direction before trigger roll animation
    private void Roll()
    {
        isRolling = true;
        //anim.SetTrigger("Roll");
        enableRotationFlag = false;

        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        switch (x,z)
        {
            case ( 0, > 0): // W
                transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y, 0);
                anim.SetTrigger("Roll");
                break;
            case (0, < 0): // S
                transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y - 180, 0);
                anim.SetTrigger("Roll");
                break;
            case (< 0, 0): // A
                transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y - 90, 0);
                anim.SetTrigger("Roll");
                break;
            case (> 0, 0): // D
                transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y + 90, 0);
                anim.SetTrigger("Roll");
                break;

            case (< 0, > 0): // WA
                transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y - 45, 0);
                anim.SetTrigger("Roll");
                break;
            case (> 0,  > 0): // WD
                transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y + 45, 0);
                anim.SetTrigger("Roll");
                break;
            case ( < 0, < 0): // SA
                transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y - 135, 0);
                anim.SetTrigger("Roll");
                break;
            case ( > 0, < 0): // SD
                transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y + 135, 0);
                anim.SetTrigger("Roll");
                break;
            default:
                Debug.Log("Switch Error");
                break;
        }


        //if (Input.GetKey(KeyCode.W))
        //{
        //    Debug.Log("JS Input.GetKey(KeyCode.W): " + Input.GetKey(KeyCode.W));
        //    transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y, 0);
        //    anim.SetTrigger("Roll");
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    Debug.Log("JS Input.GetKey(KeyCode.S): " + Input.GetKey(KeyCode.S));
        //    transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y - 180, 0);
        //    anim.SetTrigger("Roll");
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    Debug.Log("JS Input.GetKey(KeyCode.A): " + Input.GetKey(KeyCode.A));
        //    transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y - 90, 0);
        //    anim.SetTrigger("Roll");
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    Debug.Log("JS Input.GetKey(KeyCode.D): " + Input.GetKey(KeyCode.D));
        //    transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y + 90, 0);
        //    anim.SetTrigger("Roll");
        //}
        //if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        //{
        //    transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y - 45, 0);
        //    anim.SetTrigger("Roll");
        //}
        //if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        //{
        //    transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y + 45, 0);
        //    anim.SetTrigger("Roll");
        //}
        //if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        //{
        //    transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y - 135, 0);
        //    anim.SetTrigger("Roll");
        //}
        //if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        //{
        //    transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y + 135, 0);
        //    anim.SetTrigger("Roll");
        //}
    }

    // rotate the model facing the direction of mouse
    private void faceMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 200))
        {
            Vector3 targetVec = hitInfo.point;
            targetVec.y = target.position.y;
            target.LookAt(targetVec);
        }
    }
}
