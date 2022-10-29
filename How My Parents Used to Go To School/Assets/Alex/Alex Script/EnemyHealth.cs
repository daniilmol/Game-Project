using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float healthMax = 3f;
    [SerializeField] Enemy enemy;
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
        enemy.GetComponent<MeshRenderer>().material = enemy.getFlashColor();;
        Invoke("ResetColor", 0.2f);
        if (health <= 0)
        {
            if (Random.Range(0, 100) < enemy.GetDropChance())
            {
                Powerup powerUp = Instantiate(enemy.GetPowerUp(), gameObject.transform.position, Quaternion.identity).GetComponent<Powerup>();
                powerUp.Initialize(Random.Range(0, 1));
            }
            Destroy(this.gameObject);
            SpawnEnemies.numberOfEnimies--;
        }
    }

    private void ResetColor(){
        enemy.GetComponent<MeshRenderer>().material = enemy.getOriginalColor();
    }

    public void heal(float amount)
    {
        health += amount;

        if (health > healthMax)
        {
            health = healthMax;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            takeDamage(1);
        }
        //if (collision.gameObject.tag == "Bullet")
        //{
        //    takeDamage(2);
        //}
    }
}
