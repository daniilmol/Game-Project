using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool shieldFlag;
    public GameObject obj;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        shieldFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

    }

    // if shiled is hitted by enemy or bullet, set shieldFlag to false
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            shieldFlag = false;
        }
        if (collision.gameObject.tag == "Bullet")
        {
            shieldFlag = false;
        }
    }

    // set player position to shield
    private void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
    }
}
