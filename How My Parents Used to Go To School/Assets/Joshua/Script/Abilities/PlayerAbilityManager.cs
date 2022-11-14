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

    private void Start()
    {
        // init shield
        GameObject shieldClone = (GameObject)Instantiate(abilityPrefebList[0], this.transform);
        shield = shieldClone.GetComponent<Shield>();
        shield.DeActive();

        // init darkMagicAura
        GameObject darkMagicAuraClone = (GameObject)Instantiate(abilityPrefebList[1], this.transform);
        darkMagicAura = darkMagicAuraClone.GetComponent<DarkMagicAura>();
        darkMagicAura.DeActive();

        // init fireBallClone
        GameObject fireBallClone = (GameObject)Instantiate(abilityPrefebList[2], this.transform);
        fireBall = fireBallClone.GetComponent<FireBall>();
        fireBall.DeActive();

        // init damageUp
        GameObject damageUpClone = (GameObject)Instantiate(abilityPrefebList[3], this.transform);
        damageUp = damageUpClone.GetComponent<DamageUp>();

        // init speedUp
        GameObject speedUpClone = (GameObject)Instantiate(abilityPrefebList[4], this.transform);
        speedUp = speedUpClone.GetComponent<SpeedUp>();
    }

    public void ActiveShield()
    {
        shield.Active();
    }

    public void DeActiveShield()
    {
        shield.DeActive();
    }

    public void ActiveDarkMagicAura()
    {
        darkMagicAura.Active();
    }

    public void DeActiveDarkMagicAura()
    {
        darkMagicAura.DeActive();
    }

    public void ActiveFireBall()
    {
        fireBall.Active();
    }

    public void DeActiveFireBall()
    {
        fireBall.DeActive();
    }

    public void ActiveDamageUp()
    {
        damageUp.Active();
    }

    public void DeActiveDamageUp()
    {
        damageUp.DeActive();
    }

    public void ActiveSpeedUp()
    {
        speedUp.Active();
    }

    public void DeActiveSpeedUp()
    {
        speedUp.DeActive();
    }
}
