using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    private bool shieldFlag;
    [SerializeField] private GameObject shield;
    [SerializeField] private int shieldReactTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        shieldFlag = FindObjectOfType<Shield>().shieldFlag;
    }

    // Update is called once per frame
    void Update()
    {
        shieldFlag = FindObjectOfType<Shield>().shieldFlag;

        // If shieldFlag is false, disable shield and wait for a for a movment. Then enable shield gameobject
        if (shieldFlag == false)
        {
            CloseShield();
            StartCoroutine(ReactShield());
        }
    }

    // close shield
    private void CloseShield()
    {
        shield.SetActive(false);
        shield.GetComponent<Shield>().shieldFlag = true;
    }

    // reactive shield
    IEnumerator ReactShield()
    {
        yield return new WaitForSeconds(shieldReactTime);
        shield.SetActive(true);
    }
}
