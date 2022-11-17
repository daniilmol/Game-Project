using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float healthMax;
    public Image healthBar;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        healthMax = GetComponent<StatContainer>().GetMaxHealth();
        health = healthMax;
    }

    public float getHealth()
    {
        return health;
    }

    public float getHealthMax()
    {
        return healthMax;
    }

    public void SetHealth(float healthMax){
        this.healthMax = healthMax;
        health = this.healthMax;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / healthMax;
        if (health <= 0)
        {
            PlayerPrefs.SetFloat("Scale", 1f);
            PlayerPrefs.SetFloat("Damage", 1f);
            PlayerPrefs.SetFloat("Speed", 1f);
            PlayerPrefs.SetFloat("AttackSpeed", 1f);
            PlayerPrefs.SetFloat("Health", 10f);
            SceneManager.LoadScene("Stage");
        }
    }

    public void heal(float amount)
    {
        health += amount;
        healthBar.fillAmount = health / healthMax;

        if (health > healthMax)
        {
            health = healthMax;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Sword")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
    bool allowed = true;
    

    public IEnumerator StartChargeCooldown(){
        allowed = false;
        yield return new WaitForSeconds(2);
        allowed = true;
    }
}
