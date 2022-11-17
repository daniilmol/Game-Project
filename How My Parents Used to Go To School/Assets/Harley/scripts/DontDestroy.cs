using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject[] objectList;
    public static GameObject Player;
    void Awake()
    {
        Player = GameObject.Find("Player");
        if (Player == null)
        {
            DontDestroyOnLoad(Player);
        } 
        else {
            Debug.Log("Do nothing");
        }
        //foreach (var obj in objectList)
        //{
            
        //    DontDestroyOnLoad(obj);
        //}
    }
}
