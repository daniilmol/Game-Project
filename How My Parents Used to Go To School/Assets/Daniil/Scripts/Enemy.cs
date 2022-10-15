using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected GameObject player;
    protected GameObject bullet;
    [SerializeField] float difficultyScaling;
    [SerializeField] float sightRange;
    protected int health;
    protected bool canSeePlayer;
    protected bool withinPlayerRange;

    void FixedUpdate()
    {
        CheckForPlayerSight();
    }

    protected bool CheckForPlayerRange() {
        if (Vector3.Distance(transform.position, player.transform.position) > sightRange)
        {
            withinPlayerRange = false;
        }
        else 
        {
            withinPlayerRange = true;
        }
        return withinPlayerRange;
    }

    private void CheckForPlayerSight() {
        RaycastHit rayHit;
        Ray ray = new Ray(transform.position, (player.transform.position - transform.position).normalized * 10);
        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * 10);
        if (Physics.Raycast(ray, out rayHit, 100))
        {
            if (rayHit.transform.gameObject.tag == "Player")
            {
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }
        }
    }

    protected virtual void Attack() {
       
    }

    public void SetTarget(GameObject player, GameObject bullet)
    {
        this.bullet = bullet;
        this.player = player;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
