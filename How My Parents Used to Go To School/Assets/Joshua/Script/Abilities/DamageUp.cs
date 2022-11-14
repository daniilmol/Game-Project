using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour, Ability
{
    public float damage = 10;
    private StatContainer statContainer;
    private bool abilityActiveFlag = false;

    private void Awake()
    {
        statContainer = GameObject.Find("Male C").GetComponent<StatContainer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(abilityActiveFlag)
        {
            setDagame();
        }
    }

    private void setDagame()
    {
        statContainer.SetDamage(damage);
    }

    public void Active()
    {
        abilityActiveFlag = true;
    }

    public void DeActive()
    {
        abilityActiveFlag = false;
    }

    public void DisplayName()
    {
        Debug.Log("DamageUp");
    }

    public string GetName()
    {
        return "DamageUp";
    }
}
