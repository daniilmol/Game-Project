using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorSkill : ScriptableObject
{
    public string skillName;
    public float cooldownTime;
    public float activeTime;
    public bool isLearned = false;
    protected float basicDamage;
    protected StatContainer StatContainer;
    public virtual void Activate(GameObject player)
    {
    
    }

    public float GetDamage()
    {
        return basicDamage;
    }

    public void SetDamage(StatContainer sc) {
        basicDamage = sc.GetDamage();
    }

    public void SetSC(StatContainer sc) {
        StatContainer = sc;
    }

}
