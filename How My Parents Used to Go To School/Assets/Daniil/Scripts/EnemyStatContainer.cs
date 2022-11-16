using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatContainer : MonoBehaviour
{
    [SerializeField] EnemyHealth health;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float attackSpeed;
    [SerializeField] int roomId;
    
    void Start(){
    
    }
    public EnemyHealth GetHealth(){
        return health;
    }

    public float GetMaxHealth(){
        return maxHealth;
    }

    public float GetDamage(){
        return damage;
    }

    public float GetSpeed(){
        return speed;
    }

    public float GetAttackSpeed(){
        return attackSpeed;
    }

    public void IncreaseStats(float difficultyScaling){
        maxHealth *= difficultyScaling;
        damage *= difficultyScaling;
        speed *= difficultyScaling;
    }

    public int GetRoomId(){
        return roomId;
    }

    public void SetRoomId(int id){
        roomId = id;
    }
}
