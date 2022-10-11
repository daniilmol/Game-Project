using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] GameObject player;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = player.transform.position;
        Attack();
    }

    void Attack() {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 1)
        {
            Debug.Log("Attacking player");
        }
        else {
            Debug.Log("Projectile Attacking Player");
        }
    }
}
