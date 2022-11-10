using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, Ability
{
    private Renderer render;
    private SphereCollider spjereCollider;
    private bool shieldFlag = true;
    private GameObject player;
    [SerializeField] private int shieldRecoverTime = 5;
    //private float timer = 0;

    private void Awake()
    {
        render = GetComponent<Renderer>();
        spjereCollider = GetComponent<SphereCollider>();
        player = GameObject.Find("Male C");
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
            StartCoroutine(ReactShield());
        }

        //if (shieldFlag)
        //{
        //    // wait 5 sec then call functions
        //    timer += Time.deltaTime;
        //    if (timer >= shieldRecoverTime)
        //    {
        //        Debug.Log("run");
        //        ReactShield();
        //        timer = 0f;
        //    }
        //}
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
        spjereCollider.enabled = false;
    }

    // when shield is enabled, check Renderer and Collider compontents and set shieldFlag to true
    private void EnableShield()
    {
        render.enabled = true;
        spjereCollider.enabled = true;
        shieldFlag = true;
    }

    // wait for 5s then call EnableShield()
    IEnumerator ReactShield()
    {
        yield return new WaitForSeconds(shieldRecoverTime);
        EnableShield();
    }
    //private void ReactShield()
    //{
    //    EnableShield();
    //}

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
        gameObject.SetActive(true);
        Debug.Log("Active");
    }

    // disable the script
    public void DeActive()
    {
        gameObject.SetActive(false);
        Debug.Log("DeActive");
    }

    public void DisplayName()
    {
        Debug.Log("Shield");
    }
}