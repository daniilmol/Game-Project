using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRoomClear : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject[] disableObjects;
    [SerializeField] GameObject[] playAnimations;
    [SerializeField] GameObject[] enableObjects;

    // Update is called once per frame
    void Update()
    {
        if (boss == null) {
            DisableObjects();
            PlayAnimations();
            EnableObjects();
        }
    }

    void DisableObjects()
    {
        foreach (GameObject o in disableObjects)
        {
            o.SetActive(false);
        }
    }

    void PlayAnimations()
    {
        foreach (GameObject o in playAnimations)
        {
            Debug.Log("ANIMATION");
            o.GetComponent<Animator>().Play("Base Layer.Open", 0, 0.25f);
        }
    }

    void EnableObjects()
    {
        foreach (GameObject o in enableObjects)
        {
            o.SetActive(true);
        }
    }
}
