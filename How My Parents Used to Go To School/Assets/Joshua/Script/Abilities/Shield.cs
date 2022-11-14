using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, Ability
{
    private Renderer render;
    private SphereCollider sphereCollider;
    private bool shieldFlag = true;
    private GameObject player;
    [SerializeField] private int shieldRecoverTime = 5;
    private float timer = 0;
    //private bool abilityActiveFlag = false;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        sphereCollider = GetComponent<SphereCollider>();
        player = GameObject.Find("Male C");
        DisableShield();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        if (shieldFlag == false)
        {
            // wait 5 sec then call functions
            timer += Time.deltaTime;
            if (timer >= shieldRecoverTime)
            {
                EnableShield();
                timer = 0f;
            }
        }
    }

    // if shield touch enemy or bullet, disable shield and set shieldFlag to false
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            shieldFlag = false;
            DisableShield();
        }
        if (collision.gameObject.tag == "Bullet")
        {
            shieldFlag = false;
            DisableShield();
        }
    }

    // when shield is disabled, uncheck Renderer and Collider compontents
    private void DisableShield()
    {
        render.enabled = false;
        sphereCollider.enabled = false;
    }

    // when shield is enabled, check Renderer and Collider compontents and set shieldFlag to true
    private void EnableShield()
    {
        render.enabled = true;
        sphereCollider.enabled = true;
        shieldFlag = true;
    }

    // set player position to shield
    private void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
    }

    public string GetName()
    {
        return "Shield";
    }

    // enable the script
    public void Active()
    {
        EnableShield();
    }

    // disable the script
    public void DeActive()
    {
        DisableShield();
    }

    public void DisplayName()
    {
        Debug.Log("Shield");
    }
}