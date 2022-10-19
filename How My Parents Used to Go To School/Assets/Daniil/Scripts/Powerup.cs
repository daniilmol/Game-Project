using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private string name;
    private float healthBoost;
    private float damageBoost;
    private float speedBoost;

    public void Initialize(int boost)
    {
        if (boost == 0)
        {
            name = "Health Boost";
            healthBoost = 10;
        }
        else if (boost == 1)
        {
            name = "Damage Boost";
            damageBoost = 10;
        }
        else if (boost == 2) 
        {
            name = "Speed Boost";
            speedBoost = 10;
        }
    }
    public float[] getStats() {
        float[] stats = {healthBoost, damageBoost, speedBoost };
        return stats;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<PlayerHealth>().IncreaseStats(this);
            Destroy(gameObject);
        }
    }

    public string getName() 
    { 
        return name;
    }
}
