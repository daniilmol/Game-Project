using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : Enemy
{
    [SerializeField] float range = 1f;
    [SerializeField] float attackSpeed = 2f;
    private Necromancer necromancer;
    private float lastShotTime;
    private bool attacking;
    private bool following;
    private bool IsAvailable = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        damage = 2f;
        necromancer = FindObjectOfType<Necromancer>();
        player = GameObject.FindGameObjectWithTag("Player");
        dropChance = 0;
    }
    void Update()
    {
        agent.updateRotation = false;
        FaceTarget(player.transform.position);
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > range)
        {
            following = true;
            attacking = false;
        }
        else if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= range)
        {
            attacking = true;
            following = false;
        }
        SkeletonBehaviour();
    }

    private void OnDestroy() {
        necromancer.MinionDead();
    }

    private void FaceTarget(Vector3 destination)
    {
        if (following && CheckForPlayerRange()) {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
        }
    }

    private void SkeletonBehaviour()
    {
        if (following)
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
        if (!IsAvailable)
        {
            return;
        }

        StartCoroutine(StartCooldown());
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(attackSpeed);
        IsAvailable = true;
    }
}
