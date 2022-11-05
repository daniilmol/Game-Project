using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, Ability
{
    private Renderer renderer;
    private SphereCollider collider;
    private bool shieldFlag = true;
    private GameObject player;
    [SerializeField] private int shieldRecoverTime = 5;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<SphereCollider>();
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
        renderer.enabled = false;
        collider.enabled = false;
    }

    // when shield is enabled, check Renderer and Collider compontents and set shieldFlag to true
    private void EnableShield()
    {
        renderer.enabled = true;
        collider.enabled = true;
        shieldFlag = true;
    }

    // wait for 5s then call EnableShield()
    IEnumerator ReactShield()
    {
        yield return new WaitForSeconds(shieldRecoverTime);
        EnableShield();
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