using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject[] objectList;

    void Awake()
    {
        foreach (var obj in objectList)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
