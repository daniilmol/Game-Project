using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor1 = null;
    [SerializeField] private Animator myDoor2 = null;

    private void OnTriggerEnter(Collider other)
    {
        myDoor1.Play("RightDoorOpen", 0, 0.0f);
        myDoor2.Play("LeftDoorOpen", 0, 0.0f);
        Debug.Log("Play");
    }
}
