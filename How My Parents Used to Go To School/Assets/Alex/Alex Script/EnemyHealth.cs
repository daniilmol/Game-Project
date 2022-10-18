using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float healthMax = 3f;
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
            Destroy(this.gameObject);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            takeDamage(1);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(2);
        }
    }
}
