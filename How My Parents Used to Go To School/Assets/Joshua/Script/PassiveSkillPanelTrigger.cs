using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillPanelTrigger : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            canvas.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
