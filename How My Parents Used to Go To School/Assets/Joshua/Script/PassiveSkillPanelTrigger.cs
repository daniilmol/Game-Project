using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PassiveSkillPanelTrigger : MonoBehaviour
{
    private GameObject gObj;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("run");
        Debug.Log("position: " + transform.position);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        Debug.Log("position: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        gObj = GameObject.Find("PassiveSkillPanel");
        if (other.transform.tag == "Player")
        {
            gObj.GetComponent<Canvas>().enabled = true;
            Destroy(gameObject);
        }
    }
}
