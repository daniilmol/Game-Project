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
        maxHealth = PlayerPrefs.GetFloat("Health");
        damage = PlayerPrefs.GetFloat("Damage");
        speed = PlayerPrefs.GetFloat("Speed");
        attackSpeed = PlayerPrefs.GetFloat("AttackSpeed");
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

    public void SetDamage(float damage){
        this.damage = damage;
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    public void SetPlayerHealth(float healNum)
    {
        playerHealth.heal(healNum);
    }

    public void IncreaseStats(Powerup powerup){
        playerHealth.heal(powerup.getStats()[0]);
        damage += powerup.getStats()[1];
        speed += powerup.getStats()[2];
        attackSpeed /= powerup.getStats()[3];
    }

}
