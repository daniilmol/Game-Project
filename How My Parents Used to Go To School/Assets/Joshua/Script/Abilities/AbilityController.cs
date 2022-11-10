using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private Ability ability;
    public GameObject gameObj;

    // Start is called before the first frame update
    void Start()
    {
        ability = gameObj.GetComponent<DarkMagicAura>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
