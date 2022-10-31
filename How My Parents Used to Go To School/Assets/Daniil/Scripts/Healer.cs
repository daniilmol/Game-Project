using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Healer : Enemy
{
    [SerializeField] float range = 4f;
    [SerializeField] float attackSpeed = 1f;
    private bool shooting;
    private bool following;
    private bool IsAvailable = true;
    private GameObject[] otherEnemies;
    private GameObject healingTarget;

    private Hashtable hitList = new Hashtable();


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

     public IEnumerator StartCooldown() {
        IsAvailable = false;
        yield return new WaitForSeconds(attackSpeed);
        IsAvailable = true;
    }

    void Update()
    {
        agent.updateRotation = false;
        float nearestEnemyDistance = 1000;
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        hitList.Clear();
        HealerBehaviour(playerDistance, nearestEnemyDistance);
    }

    private void Heal(){
        int layerMask = 1 << 7;
        Collider collider = GetComponent<Collider>();
        Collider[] cal = Physics.OverlapSphere(collider.bounds.center, collider.transform.localScale.x*4, layerMask);
        Debug.Log(collider.transform.localScale.x * 4);
        int count = 0;
        foreach (Collider c in cal)
        {
            if (!hitList.ContainsKey(c.GetInstanceID()))
            {
               
                hitList.Add(c.GetInstanceID(), true);
                if (c != collider)
                {
                    c.GetComponent<EnemyHealth>().heal(1);
                    StartCoroutine(StartCooldown());
                }
            }
        }
    }

    private void FaceTarget(Vector3 destination)
    {
        
    }

    private void HealerBehaviour(float playerDistance, float nearestEnemyDistance) {
        if(!IsAvailable){
            return;
        }
        otherEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float currentLowest = nearestEnemyDistance;
        for(int i = 0; i < otherEnemies.Length; i++){
            if(otherEnemies[i].GetComponent<EnemyHealth>().getHealth() < otherEnemies[i].GetComponent<EnemyHealth>().getHealthMax() 
                && Vector3.Distance(otherEnemies[i].transform.position, transform.position) < playerDistance
                && Vector3.Distance(otherEnemies[i].transform.position, transform.position) < currentLowest){
                agent.destination = otherEnemies[i].transform.position;
                healingTarget = otherEnemies[i];
            }
        }
        if(healingTarget != null && Vector3.Distance(healingTarget.transform.position, transform.position) < range){
            agent.destination = transform.position;
            Heal();
        }
        if (following && CheckForPlayerRange())
        {
            agent.SetDestination(player.transform.position); 
        }
        else if (shooting && canSeePlayer) {
            agent.SetDestination(gameObject.transform.position);
            Attack();
        }
    }
}
