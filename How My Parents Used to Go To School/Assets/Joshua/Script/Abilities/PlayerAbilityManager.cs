using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    [SerializeField] GameObject[] abilityPrefebList;
    //private Ability[] abilities;
    private List<Ability> abilities = new List<Ability>();

    private void Start()
    {
        //Instantiate(abilityPrefebList[0], this.transform);
        GeneratePrefeb();
        abilities.Add(abilityPrefebList[0].GetComponent<Shield>());
    }

    private void Update()
    {
        //abilityPrefebList[0].GetComponent<Shield>().DeActive();
    }

    private void GeneratePrefeb()
    {
        foreach(GameObject prefeb in abilityPrefebList)
        {
            Instantiate(prefeb, this.transform);
        }
    }
}
