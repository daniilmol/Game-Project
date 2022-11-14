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
        healthMax = enemy.GetComponent<EnemyStatContainer>().GetMaxHealth();
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

    public void ScaleHealth(float scale){
        healthMax += healthMax * enemy.GetScale();
        health = healthMax;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        Debug.Log("DDD" + amount);
        enemy.GetComponent<MeshRenderer>().material = enemy.getFlashColor();;
        Invoke("ResetColor", 0.2f);
        if (health <= 0)
        {
            if (Random.Range(0, 100) < enemy.GetDropChance())
            {
                Quaternion powerUpRotation = enemy.GetPowerUp().transform.rotation;
                Powerup powerUp = Instantiate(enemy.GetPowerUp(), gameObject.transform.position, powerUpRotation).GetComponent<Powerup>();
                powerUp.Initialize(Random.Range(0, 4));
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
}
