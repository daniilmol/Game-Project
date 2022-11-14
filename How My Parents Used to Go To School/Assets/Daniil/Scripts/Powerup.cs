using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Powerup : MonoBehaviour
{
    private string name;
    private float healthBoost;
    private float damageBoost;
    private float speedBoost;
    private float attackSpeedBoost = 1;
    private GameObject player;
    [SerializeField] TextMeshProUGUI powerUpText;
    [SerializeField] GameObject textContainer;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        if(Vector3.Distance(player.transform.position, transform.position) < 6f){
            textContainer.SetActive(true);
        }else{
            textContainer.SetActive(false);
        }
    }

    public void Initialize(int boost)
    {
       // boost = 3;
        if (boost == 0)
        {
            name = "Health +1";
            healthBoost = 1;
        }
        else if (boost == 1)
        {
            name = "Damage +1";
            damageBoost = 1f;
        }
        else if (boost == 2) 
        {
            name = "Speed +10%";
            speedBoost = 0.1f;
        }
        else if(boost == 3)
        {
            name = "Attack Speed +10%";
            attackSpeedBoost = 0.9f;
        }
        powerUpText.text = name;
    }
    public float[] getStats() {
        float[] stats = {healthBoost, damageBoost, speedBoost, attackSpeedBoost};
        return stats;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<StatContainer>().IncreaseStats(this);
            print("Picked up " + name);
            Destroy(gameObject);
        }
    }

    public string getName() 
    { 
        return name;
    }
}
