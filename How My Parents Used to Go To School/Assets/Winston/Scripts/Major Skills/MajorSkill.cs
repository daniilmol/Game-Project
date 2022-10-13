using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorSkill : ScriptableObject
{
    public string skillName;
    public float cooldownTime;
    public float activeTime;
    public bool isLearned = false;

    public virtual void Activate(GameObject player)
    {
    
    }

    public virtual void whirlwindSpin(GameObject player) { 
    
    
    }
}
