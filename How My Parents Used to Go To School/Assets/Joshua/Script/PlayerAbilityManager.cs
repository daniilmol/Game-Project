using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    private bool shieldFlag;
    [SerializeField] private GameObject shield;
    [SerializeField] private int shieldReactTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        shieldFlag = FindObjectOfType<Shield>().shieldFlag;
    }

    // Update is called once per frame
    void Update()
    {
        ShieldAbility(FindObjectOfType<Shield>().shieldFlag);
    }

    // enable generate shield. When shield touch enemy or bullet, shield disappear and recover after a period of time
    private void ShieldAbility(bool flag)
    {
        if(flag == false)
        {
            CloseShield();
            StartCoroutine(ReactShield());
        }
    }

    // close shield if it is hitted by enemy or bullet
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
