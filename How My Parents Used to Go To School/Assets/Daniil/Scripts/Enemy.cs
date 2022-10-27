using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected GameObject player;
    protected GameObject bullet;
    protected float dropChance = 100;
    protected int health;
    protected float damage;
    protected bool canSeePlayer;
    protected bool withinPlayerRange;
    protected RaycastHit rayHit;
    [SerializeField] float difficultyScaling;
    [SerializeField] float sightRange;
    [SerializeField] GameObject powerUpDrop;

    void FixedUpdate()
    {
        CheckForPlayerSight();
    }

    void Update(){
        CheckSpeed();
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

    public GameObject GetPowerUp() {
        return powerUpDrop;
    }

    public float GetDropChance() {
        return dropChance;
    }

    public void OnDeath() {
        if (Random.Range(0, 100) < dropChance) {
            Powerup powerUp = Instantiate(powerUpDrop, gameObject.transform.position, Quaternion.identity).GetComponent<Powerup>();
            powerUp.Initialize(Random.Range(0, 3));
        }
        Destroy(gameObject);
    }

    private void CheckSpeed(){
        if(GetComponent<Rigidbody>().velocity == Vector3.zero){
            agent.isStopped = false;
        }
    }


}
