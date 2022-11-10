using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    [SerializeField] GameObject[] abilityPrefebList;
    private List<Ability> abilities = new List<Ability>();
    GameObject go;
    private void Start()
    {
        //Instantiate(abilityPrefebList[0], this.transform);
        GeneratePrefeb();

        //GameObject clone = (GameObject)Instantiate(abilityPrefebList[2]);

        //Ability ab = clone.GetComponent<FireBall>();

        //ab.DisplayName();
        //ab.DeActive();

        //go = abilityPrefebList[1];

        //Instantiate(go, this.transform);

        //abilities.Add(abilityPrefebList[0].GetComponent<Shield>());
        //abilities.Add(abilityPrefebList[1].GetComponent<DarkMagicAura>());
    }

    private void Update()
    {
        //abilityPrefebList[0].GetComponent<Shield>().DeActive();
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Debug.Log("run");
        //    go.GetComponent<DarkMagicAura>().DisplayName();
        //}
    }

    private void GeneratePrefeb()
    {
        foreach(GameObject prefeb in abilityPrefebList)
        {
            Instantiate(prefeb, this.transform);
        }
    }
}
