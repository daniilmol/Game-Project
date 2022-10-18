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

   // public float[] IncreaseStats() { 
        
       // return 
    ////}

    public string getName() 
    { 
        return name;
    }
}
