using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatContainer : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float attackSpeed;

    void Start(){
        playerHealth.SetHealth(maxHealth);
    }

    public PlayerHealth GetHealth(){
        return playerHealth;
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

    public float GetMaxHealth(){
        return maxHealth;
    }

    public void IncreaseStats(Powerup powerup){
        playerHealth.heal(powerup.getStats()[0]);
        damage += powerup.getStats()[1];
        speed += powerup.getStats()[2];
        attackSpeed *= powerup.getStats()[3];
    }
}
