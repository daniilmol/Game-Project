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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag != "Projectile")
    //    {
    //        gameObject.transform.parent = collision.gameobject.transform;
    //        Destroy(GetComponent<Rigidbody>());
    //        GetComponent<CircleCollider2D>().enabled = false;
    //    }

    //    if (collision.tag == "Player")
    //    {
    //        var healthComponent = collision.GetComponenet<health>();
    //        if (healthComponent != null)
    //        {
    //            healthComponent.TakeDamage(1);
    //        }
    //    }
    //}
}
