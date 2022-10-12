using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime = 3f;

    private void Start()
    {
        StartLifetime();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator StartLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
