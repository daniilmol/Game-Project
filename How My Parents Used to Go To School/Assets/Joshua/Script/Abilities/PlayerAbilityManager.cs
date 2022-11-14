using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    [SerializeField] GameObject[] abilityPrefebList;
    //private List<Ability> abilities = new List<Ability>();
    private List<GameObject> gObjList = new List<GameObject>();
    private Ability shield;
    private Ability darkMagicAura;
    private Ability fireBall;
    private Ability damageUp;
    private Ability speedUp;

    private void Start()
    {
        GameObject shieldClone = (GameObject)Instantiate(abilityPrefebList[0], this.transform);
        shield = shieldClone.GetComponent<Shield>();
        shield.Active();

        GameObject darkMagicAuraClone = (GameObject)Instantiate(abilityPrefebList[1], this.transform);
        darkMagicAura = darkMagicAuraClone.GetComponent<DarkMagicAura>();
        darkMagicAura.Active();

        GameObject fireBallClone = (GameObject)Instantiate(abilityPrefebList[2], this.transform);
        fireBall = fireBallClone.GetComponent<FireBall>();
        fireBall.Active();

        GameObject damageUpClone = (GameObject)Instantiate(abilityPrefebList[3], this.transform);
        damageUp = damageUpClone.GetComponent<DamageUp>();
        damageUp.Active();

        GameObject speedUpClone = (GameObject)Instantiate(abilityPrefebList[4], this.transform);
        speedUp = speedUpClone.GetComponent<SpeedUp>();
        speedUp.Active();
    }

    private void Update()
    {

    }

    private void GeneratePrefeb()
    {
        foreach(GameObject prefeb in abilityPrefebList)
        {
            gObjList.Add(Instantiate(prefeb, this.transform));
        }
    }
}
