using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSphere : MonoBehaviour
{
    public ParticleSystem particle;
    private Rigidbody rb;
    private float timer = 0;
    private StatContainer statContainer;
    public float damege;

    // Start is called before the first frame update
    void Start()
    {
        particle.Stop();
        rb = transform.GetComponent<Rigidbody>();
        statContainer = GameObject.Find("Male C").GetComponent<StatContainer>();
        damege = statContainer.GetDamage();
    }

    // Update is called once per frame
    void Update()
    {
        //if (abilityActiveFlag)
        //{
        //    particle.Play();
        //}

        // wait 5 sec then call functions
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            StopFireBallMove();
            particle.Play();
            HidenSphere();
            StartCoroutine(DistorySphere());
            timer = 0f;
        }
    }

    // when the sphere hit enemy, stop and hide the sphere.
    // Then play particle. After 1 sec destory sphere
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StopFireBallMove();
            particle.Play();
            HidenSphere();
            StartCoroutine(DistorySphere());
            other.gameObject.GetComponent<EnemyHealth>().takeDamage(damege);
        }
    }

    // hiden sphere
    private void HidenSphere()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // when sphere hit enemy, stop the sphere
    private void StopFireBallMove()
    {
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints = RigidbodyConstraints.None;
    }

    // destory sphere
    IEnumerator DistorySphere()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
