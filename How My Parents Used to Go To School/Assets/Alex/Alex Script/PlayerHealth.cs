using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float healthMax;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
    }

    public float getHealth()
    {
        return health;
    }

    public float getHealthMax()
    {
        return healthMax;
    }

    public void takeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            // Dead
            //anim.SetBool("IsDead", true);
        }
    }

    public void heal(float amount)
    {
        health += amount;

        if (health > healthMax)
        {
            health = healthMax;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Sword")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            takeDamage(1);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(2);
        }
    }

    public void IncreaseStats(Powerup powerup) {
        health += powerup.getStats()[0];
       // damage += powerup.getStats()[1];
        //speed += powerup.getStats()[2];
    }
}
