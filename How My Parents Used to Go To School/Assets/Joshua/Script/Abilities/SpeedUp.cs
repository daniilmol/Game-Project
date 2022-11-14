using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour, Ability
{
    public float speed = 1;
    private StatContainer statContainer;

    private void Awake()
    {
        statContainer = GameObject.Find("Male C").GetComponent<StatContainer>();
    }

    // increase player speed
    private void IncreaseSpeed()
    {
        float tmp = statContainer.GetSpeed() + 1;
        statContainer.SetSpeed(tmp);
    }

    // decrease player speed
    private void DecreaseSpeed()
    {
        float tmp = statContainer.GetSpeed() - 1;
        statContainer.SetSpeed(tmp);
    }

    public void Active()
    {
        IncreaseSpeed();
    }

    public void DeActive()
    {
        DecreaseSpeed();
    }

    public void DisplayName()
    {
        Debug.Log("SpeedUp");
    }

    public string GetName()
    {
        return "SpeedUp";
    }
}
