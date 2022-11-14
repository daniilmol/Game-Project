using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour, Ability
{
    public float increaseDamage = 1;
    private StatContainer statContainer;

    private void Awake()
    {
        statContainer = GameObject.Find("Male C").GetComponent<StatContainer>();
        //damage = statContainer.GetDamage();
    }

    // increase player damage
    private void addDagame()
    {
        statContainer.SetDamage(statContainer.GetDamage() + increaseDamage);
    }

    // decrease player damage
    private void minsDagame()
    {
        statContainer.SetDamage(statContainer.GetDamage() - increaseDamage);
    }

    public void Active()
    {
        addDagame();
    }

    public void DeActive()
    {
        minsDagame();
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
