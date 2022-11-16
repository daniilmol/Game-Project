using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gunner : Enemy
{
    [SerializeField] float range = 7f;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] bool displayDistance = false;
    private bool shooting;
    private bool following;
    private bool IsAvailable = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        dropChance = 5f;
    }

    void Update()
    {
        agent.updateRotation = false;
        FaceTarget(player.transform.position);
        if(displayDistance){
            //print("GUNNER PLAYER DISTANCE: " + Vector3.Distance(gameObject.transform.position, player.transform.position));
        }
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > range)
        {
            following = true;
            shooting = false;
        }
        else if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= range) {
            shooting = true;
            following = false;
        }
        if(displayDistance){
            print("The gunner is shooting: " + shooting + " The gunner is following: " + following);
        }
        GunnerBehaviour();
    }

    private void FaceTarget(Vector3 destination)
    {
        if ((following || shooting) && CheckForPlayerRange()) {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);
        }
    }

    private void GunnerBehaviour() {
        //Debug.Log(canSeePlayer);
        if (following && CheckForPlayerRange())
        {
            agent.SetDestination(player.transform.position); 
        }
        else if (shooting && canSeePlayer) {
            agent.SetDestination(gameObject.transform.position);
            Attack();
        }
    }

    protected override void Attack() {
        if (!IsAvailable) {
            return;
        }
        GameObject particle = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
        particle.GetComponent<Bullet>().SetShooter(gameObject);
        particle.GetComponent<Rigidbody>().AddForce(transform.forward * particle.GetComponent<Bullet>().GetSpeed(), ForceMode.Impulse);
        StartCoroutine(StartCooldown());
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(attackSpeed);
        IsAvailable = true;
    }
}
