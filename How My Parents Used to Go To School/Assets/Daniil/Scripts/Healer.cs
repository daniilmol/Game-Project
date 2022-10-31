using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Healer : Enemy
{
    [SerializeField] float range = 7f;
    [SerializeField] float attackSpeed = 1f;
    private bool shooting;
    private bool following;
    private bool IsAvailable = true;
    private GameObject[] otherEnemies;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(LateStart(1));
    }

    IEnumerator LateStart(float waitTime)
     {
         yield return new WaitForSeconds(waitTime);
         otherEnemies = GameObject.FindGameObjectsWithTag("Enemy");
     }

    void Update()
    {
        agent.updateRotation = false;
        float nearestEnemyDistance = 1000;
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        HealerBehaviour(playerDistance, nearestEnemyDistance);
    }

    private void HealerBehaviour(float playerDistance, float nearestEnemyDistance) {
        for(int i = 0; i < otherEnemies.Length; i++){
            if(otherEnemies[i].GetComponent<EnemyHealth>().getHealth() < otherEnemies[i].GetComponent<EnemyHealth>().getHealthMax() 
                && Vector3.Distance(otherEnemies[i].transform.position, transform.position) < playerDistance){
                agent.destination = otherEnemies[i].transform.position;
            }
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
