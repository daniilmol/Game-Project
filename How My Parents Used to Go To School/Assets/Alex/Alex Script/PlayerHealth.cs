using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float healthMax;
    public Image healthBar;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        healthMax = GetComponent<StatContainer>().GetMaxHealth();
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

    public void SetHealth(float healthMax){
        this.healthMax = healthMax;
        health = this.healthMax;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / healthMax;
        if (health <= 0)
        {
            // Dead
            //anim.SetBool("IsDead", true);
        }
    }

    public void heal(float amount)
    {
        health += amount;
        healthBar.fillAmount = health / healthMax;

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
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().isBoss())
        {
            takeDamage(5);
            //c.GetComponent<NavMeshAgent>().isStopped = true;
            float force = 30f;
            GetComponent<Rigidbody>().AddForce(force * -collision.gameObject.transform.forward, ForceMode.Impulse);                    //  Need a on hit decrese health function here, example: decreaseHealth(Gameobject enemy);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            //takeDamage(collision.gameObject.GetComponent<Bullet>().GetShooter().GetComponent<EnemyStatContainer>().GetDamage());
            //print("Player took " + collision.gameObject.GetComponent<Bullet>().GetShooter().GetComponent<EnemyStatContainer>().GetDamage());
        }
    }
}
