using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    [SerializeField] GameObject[] abilityPrefebList;
    private Ability shield;
    private Ability darkMagicAura;
    private Ability fireBall;
    private Ability damageUp;
    private Ability speedUp;
    public List<Ability> unactivedAbilityList = new List<Ability>();

    private void Start()
    {
        // init shield
        GameObject shieldClone = (GameObject)Instantiate(abilityPrefebList[0], this.transform);
        shield = shieldClone.GetComponent<Shield>();
        shield.DeActive();
        unactivedAbilityList.Add(shield);

        // init darkMagicAura
        GameObject darkMagicAuraClone = (GameObject)Instantiate(abilityPrefebList[1], this.transform);
        darkMagicAura = darkMagicAuraClone.GetComponent<DarkMagicAura>();
        darkMagicAura.DeActive();
        unactivedAbilityList.Add(darkMagicAura);

        // init fireBallClone
        GameObject fireBallClone = (GameObject)Instantiate(abilityPrefebList[2], this.transform);
        fireBall = fireBallClone.GetComponent<FireBall>();
        fireBall.DeActive();
        unactivedAbilityList.Add(fireBall);

        // init damageUp
        GameObject damageUpClone = (GameObject)Instantiate(abilityPrefebList[3], this.transform);
        damageUp = damageUpClone.GetComponent<DamageUp>();
        unactivedAbilityList.Add(damageUp);

        // init speedUp
        GameObject speedUpClone = (GameObject)Instantiate(abilityPrefebList[4], this.transform);
        speedUp = speedUpClone.GetComponent<SpeedUp>();
        unactivedAbilityList.Add(speedUp);

        //ActiveShield();
        //ActiveDarkMagicAura();
        //ActiveFireBall();

        //ActiveDamageUp();
        //ActiveSpeedUp();

        //for (int i = 0; i < unactivedAbilityList.Count; i++)
        //{
        //    Debug.Log("unactivedAbilityList: " + unactivedAbilityList[i].GetName());
        //}

    }

    public void ActiveShield()
    {
        shield.Active();
        unactivedAbilityList.Remove(shield);
        Debug.Log("ActiveShield");
    }

    public void DeActiveShield()
    {
        shield.DeActive();
        unactivedAbilityList.Add(shield);
    }

    public void ActiveDarkMagicAura()
    {
        darkMagicAura.Active();
        unactivedAbilityList.Remove(darkMagicAura);
        Debug.Log("ActiveDarkMagicAura");
    }

    public void DeActiveDarkMagicAura()
    {
        darkMagicAura.DeActive();
        unactivedAbilityList.Add(darkMagicAura);
    }

    public void ActiveFireBall()
    {
        fireBall.Active();
        unactivedAbilityList.Remove(fireBall);
        Debug.Log("ActiveFireBall");
    }

    public void DeActiveFireBall()
    {
        fireBall.DeActive();
        unactivedAbilityList.Add(fireBall);
    }

    public void ActiveDamageUp()
    {
        damageUp.Active();
        unactivedAbilityList.Remove(damageUp);
    }

    public void DeActiveDamageUp()
    {
        damageUp.DeActive();
        unactivedAbilityList.Add(damageUp);
    }

    public void ActiveSpeedUp()
    {
        speedUp.Active();
        unactivedAbilityList.Remove(speedUp);
    }

    public void DeActiveSpeedUp()
    {
        speedUp.DeActive();
        unactivedAbilityList.Add(speedUp);
    }
}
