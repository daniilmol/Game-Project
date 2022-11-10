using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem_Fire_Explosion : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowGameObject();
    }

    private void FollowGameObject()
    {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 1, obj.transform.position.z);
    }
}
