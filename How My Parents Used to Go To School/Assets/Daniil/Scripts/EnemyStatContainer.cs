using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatContainer : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] EnemyHealth health;
=======
    [SerializeField] EnemyHealth enemyHealth;
>>>>>>> Stashed changes
    [SerializeField] float maxHealth;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float attackSpeed;
<<<<<<< Updated upstream
    
    public EnemyHealth GetHealth(){
        return health;
    }

    public float GetMaxHealth(){
        return maxHealth;
=======

    public EnemyHealth GetHealth(){
        return enemyHealth;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

    public void IncreaseStats(float difficultyScaling){
        maxHealth *= difficultyScaling;
        damage *= difficultyScaling;
        speed *= difficultyScaling;
    }
=======
>>>>>>> Stashed changes
}
