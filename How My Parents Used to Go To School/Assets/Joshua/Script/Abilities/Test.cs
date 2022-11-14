using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private PlayerAbilityManager pam;
    private StatContainer sc;

    // Start is called before the first frame update
    void Start()
    {
        pam = GameObject.Find("Player").GetComponent<PlayerAbilityManager>();
        sc = GameObject.Find("Male C").GetComponent<StatContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            pam.ActiveSpeedUp();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            pam.DeActiveSpeedUp();
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("GetDamage" + sc.GetSpeed());
            Debug.Log("GetHealth" + sc.GetHealth().getHealth());
        }
    }
}
