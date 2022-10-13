using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    // Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSPeed;

    private Vector3 moveDirection;

    // References
    private CharacterController controller;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // StartCoroutine(Roll());
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

    private IEnumerator Roll()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Roll Layer"), 1);
        anim.SetTrigger("Roll");

        transform.position = transform.position;

        yield return new WaitForSeconds(2);
        anim.SetLayerWeight(anim.GetLayerIndex("Roll Layer"), 0);
    }
}
