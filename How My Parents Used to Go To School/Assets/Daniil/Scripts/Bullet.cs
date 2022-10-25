using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime = 3f;
    [SerializeField] float speed = 5f;

    void Start()
    {
        StartCoroutine(StartLifetime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            Destroy(gameObject);
        }
    }

    public IEnumerator StartLifetime()
    {
        Debug.Log("Started lifetime counter");
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public float GetSpeed(){
        return speed;
    }
}
