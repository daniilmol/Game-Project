using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPos : MonoBehaviour
{
    public GameObject startPoint;
    void Awake()
    {
        var player = GameObject.Find("Player");
        player.transform.position = startPoint.transform.position;
    }
}
