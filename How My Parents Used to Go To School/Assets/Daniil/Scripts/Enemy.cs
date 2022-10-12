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
    protected int health;
    
    void Start()
    {
    }

    void Update()
    {

    }

    protected virtual void Attack() {
       
    }

    public void SetTarget(GameObject player, GameObject bullet)
    {
        this.bullet = bullet;
        this.player = player;
        bullet.AddComponent<Bullet>();  
    }
}
