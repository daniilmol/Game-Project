using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour, Ability
{
    public float SearchRadius;

    [SerializeField] private GameObject perfeb;
    [SerializeField] private float coldDownTime = 5;

    private GameObject player;
    private Vector3 enemyPosition;
    private float enemyDistance;
    private bool fireBallFlag = false;
    private bool abilityActiveFlag = true;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Male C");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        if(abilityActiveFlag)
        {
            // wait 5 sec then call functions
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                SearchNearUnits();
                GenerateFireBall();
                timer = 0f;
            }
        }
    }

    // set player position to FireBall gameobject
    private void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
    }

    // search gameobject with enemy tag.
    public void SearchNearUnits()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, SearchRadius);

        enemyDistance = 100;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy" && DistanceCaculation(colliders[i].gameObject.transform.position) < enemyDistance)
            {
                enemyPosition = colliders[i].gameObject.transform.position;
                fireBallFlag = true;
            }
        }
    }

    // caculate distanc between enemy tag gameobject and player
    private float DistanceCaculation(Vector3 position)
    {
        float distance = (position - transform.position).magnitude;

        return distance;
    }

    // generate a sphere and move the sphere forward to a gameobject with enemy tag
    private void GenerateFireBall()
    {
        if(fireBallFlag)
        {
            GameObject fireBall = Instantiate(perfeb);
            fireBall.transform.position = new Vector3(player.transform.position.x + Mathf.Sign(enemyPosition.x), player.transform.position.y + 2, player.transform.position.z + Mathf.Sign(enemyPosition.z));
            Vector3 targetPosition = enemyPosition - fireBall.transform.position;
            fireBall.GetComponent<Rigidbody>().AddForce(targetPosition, ForceMode.Impulse);
            fireBallFlag = false;
        }
    }

    public string GetName()
    {
        return "FireBall";
    }

    public void Active()
    {
        abilityActiveFlag = true;
    }

    public void DeActive()
    {
        abilityActiveFlag = false;
    }

    public void DisplayName()
    {
        Debug.Log("FireBall");
    }
}
