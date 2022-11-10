using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMagicAura : MonoBehaviour, Ability
{
    public ParticleSystem ps;
    public float SearchRadius;
    public float damege = 1;
    private GameObject player;
    private bool abilityActiveFlag;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Male C");
        abilityActiveFlag = false;
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        if (Input.GetKeyDown(KeyCode.F))
        {
            abilityActiveFlag = true;
        }

        if (abilityActiveFlag)
        {
            EnableAbility();
            SearchNearUnits();
            StartCoroutine(DisableAbility());
            abilityActiveFlag = false;
        }
    }

    // search gameobject with "Enemy" tag. Then trigger the takeDamage function
    public void SearchNearUnits()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, SearchRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                colliders[i].gameObject.GetComponent<EnemyHealth>().takeDamage(damege);
            }
        }
    }

    // play the particle system
    private void EnableAbility()
    {
        ps.Play();
    }

    // stop the particle system
    IEnumerator DisableAbility()
    {
        yield return new WaitForSeconds(0.2f);
        ps.Stop();
    }

    // return script class name
    public string GetName()
    {
        return "DarkMagicAura";
    }

    public void Active()
    {
        abilityActiveFlag = true;
    }

    public void DeActive()
    {
        abilityActiveFlag = false;
    }

    // console log script class name
    public void DisplayName()
    {
        Debug.Log("DarkMagicAura");
    }

    // set player position to shield
    private void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
    }
}
