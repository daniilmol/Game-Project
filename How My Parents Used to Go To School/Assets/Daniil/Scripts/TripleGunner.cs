using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TripleGunner : Enemy
{
    [SerializeField] float range = 7f;
    [SerializeField] float attackSpeed = 1f;
    private bool shooting;
    private bool following;
    private bool IsAvailable = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        dropChance = 10;
    }
    void Update()
    {
        agent.updateRotation = false;
        FaceTarget(player.transform.position);
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > range)
        {
            following = true;
            shooting = false;
        }
        else if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= range) {
            shooting = true;
            following = false;
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
            GameObject particle2 = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            GameObject particle3 = Instantiate(bullet, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            particle.GetComponent<Rigidbody>().AddForce(transform.forward * particle.GetComponent<Bullet>().GetSpeed(), ForceMode.Impulse);
            
            //Vector3 angled = Quaternion.Euler(45f, 45f, 0) * transform.forward;
            //Vector3 angle2 = Quaternion.Euler(-45f, -45f, 0) * transform.forward;

            particle2.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(-45, -45, 0) * transform.forward * particle.GetComponent<Bullet>().GetSpeed(), ForceMode.Impulse);
            particle3.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(45, 45, 0) * transform.forward * particle.GetComponent<Bullet>().GetSpeed(), ForceMode.Impulse);
            StartCoroutine(StartCooldown());
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(attackSpeed);
        IsAvailable = true;
    }
}
