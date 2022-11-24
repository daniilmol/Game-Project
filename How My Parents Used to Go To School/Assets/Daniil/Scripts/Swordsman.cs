using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swordsman : Enemy
{
    [SerializeField] float range = 1f;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] Transform cube;
    [SerializeField] bool debug;
    private Animator animator;
    private float restTimer;
    private float lastRestTimerEnd;
    private float lastShotTime;
    private bool attacking;
    private bool following;
    private bool IsAvailable = true;
    private bool switchedToAttack;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        dropChance = 10f;
        restTimer = 0.3f;
        switchedToAttack = false;
    }
    void Update()
    {
        UpdateAnimator();
        agent.updateRotation = false;
        FaceTarget(player.transform.position);
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) - 1 > range  && CheckForPlayerRange())
        {
            following = true;
            attacking = false;
            switchedToAttack = false;
            lastRestTimerEnd = 0.3f;
        }
        else if (Vector3.Distance(gameObject.transform.position, player.transform.position) - 1 <= range)
        {
            attacking = true;
            following = false;
        }
        SwordsmanBehaviour();
    }

    private void UpdateAnimator(){
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("fowardSpeed", speed);
    }

    private void FaceTarget(Vector3 destination)
    {
        if (following) {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
        }
    }

    private void SwordsmanBehaviour()
    {
        if (following && CheckForPlayerRange())
        {
            agent.SetDestination(player.transform.position);
        }
        else if (attacking)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        agent.SetDestination(transform.position);
        if (!IsAvailable)
        {
            return;
        }
        if(!switchedToAttack){
            StartCoroutine(StartRestCooldown());
            print("AFTER START REST COOLDOWN");
        }
        if(lastRestTimerEnd == 0){
            SwingForward();
            StartCoroutine(StartCooldown());
        }
    }

    private void SwingForward(){
        if(debug){
            print("TANK IS ATTACKING THE PLAYER");
        }
        animator.SetTrigger("attack");
    }
    void Hit(){
        int layerMask = 1 << 9;
        Collider collider = cube.GetComponent<Collider>();
        Collider[] cal = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents * 2, Quaternion.identity, layerMask);
        Hashtable hitList = new Hashtable();
        //Debug.Log("youyou");
        bool isHit = false;
        int count = 0;
        foreach (Collider c in cal)
        {
            if (!hitList.ContainsKey(c.GetInstanceID()))
            {
                hitList.Add(c.GetInstanceID(), true);
                if (c != GetComponent<Collider>())
                {
                    if(debug){
                        print("c is null? " + c);
                        print("playerhealth is null? " + c.GetComponent<PlayerHealth>());
                        print("enemystats is null? " + GetComponent<EnemyStatContainer>().GetDamage());

                    }
                    c.GetComponent<PlayerHealth>().takeDamage(GetComponent<EnemyStatContainer>().GetDamage());


                    float force = 6;
                    Vector3 vectorForce = Vector3.Normalize(transform.position - c.transform.position);
                    //c.GetComponent<NavMeshAgent>().isStopped = true;
                    c.GetComponent<Rigidbody>().AddForce(force * -c.transform.forward, ForceMode.Impulse);

                    count++;
                }
            }
            //Debug.Log("Length" + count );
            //OnDrawGizmos(collider);
            //   Debug.Log(collider.name);
            //Debug.Log(hitList);
        }
        animator.ResetTrigger("attack");
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(attackSpeed);
        IsAvailable = true;
    }
    public IEnumerator StartRestCooldown()
    {
        yield return new WaitForSeconds(restTimer);
        lastRestTimerEnd = 0;  
        switchedToAttack = true;   
    }
}
