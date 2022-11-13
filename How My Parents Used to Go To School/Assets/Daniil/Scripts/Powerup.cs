using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private string name;
    private float healthBoost;
    private float damageBoost;
    private float speedBoost;
    private float attackSpeedBoost = 1;

    public void Initialize(int boost)
    {
       // boost = 3;
        if (boost == 0)
        {
            name = "Health Boost";
            healthBoost = 1;
        }
        else if (boost == 1)
        {
            name = "Damage Boost";
            damageBoost = 1;
        }
        else if (boost == 2) 
        {
            name = "Speed Boost";
            speedBoost = 0.1f;
        }
        else if(boost == 3)
        {
            name = "Attack Speed Boost";
            attackSpeedBoost = 0.9f;
        }
    }
    public float[] getStats() {
        float[] stats = {healthBoost, damageBoost, speedBoost, attackSpeedBoost};
        return stats;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<StatContainer>().IncreaseStats(this);
            print("Picked up " + name);
            Destroy(gameObject);
        }
    }

    public string getName() 
    { 
        return name;
    }
}
