using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float healthMax = 3f;
    [SerializeField] Enemy enemy;
    public Image healthBar;
    public Animator anim;

    private int roomId;
    //private FamilyTree tree;

    // Start is called before the first frame update
    void Start()
    {
        healthMax = enemy.GetComponent<EnemyStatContainer>().GetMaxHealth();
        roomId = enemy.GetComponent<EnemyStatContainer>().GetRoomId();
        health = healthMax;
        //tree = GetComponent<PlayerController>().GetTheTree();
    }

    public float getHealth()
    {
        return health;
    }

    public float getHealthMax()
    {
        return healthMax;
    }

    public void ScaleHealth(float scale){
        healthMax += healthMax * enemy.GetScale();
        health = healthMax;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        Debug.Log("DDD" + amount);
        enemy.GetComponent<MeshRenderer>().material = enemy.getFlashColor();;
        Invoke("ResetColor", 0.2f);
        healthBar.fillAmount = health / healthMax;
        if (health <= 0)
        {
            bool bossFlag = gameObject.GetComponent<Enemy>().isBoss();
            if (bossFlag)
            {
                Vector3 pos = gameObject.transform.position;
                pos -= new Vector3(0, transform.lossyScale.y, 0);
                Quaternion powerUpRotation = enemy.GetPowerUp().transform.rotation;
                Instantiate(enemy.GetPowerUp(), pos, powerUpRotation);
                Destroy(this.gameObject);
            }
            else if (Random.Range(0, 100) < enemy.GetDropChance())
            {
                Quaternion powerUpRotation = enemy.GetPowerUp().transform.rotation;
                Powerup powerUp = Instantiate(enemy.GetPowerUp(), gameObject.transform.position, powerUpRotation).GetComponent<Powerup>();
                powerUp.Initialize(Random.Range(0, 4));
            }
            
            Destroy(this.gameObject);
            //tree.GetExp(10);
            var ms = GameObject.Find("Generater").GetComponent<MapSystem>();
            ms.mapData.roomDataDic[roomId].monsters.Remove(enemy.gameObject);
            ms.mapData.roomDataDic[roomId].monstersCount--;
        }
    }

    private void ResetColor(){
        enemy.GetComponent<MeshRenderer>().material = enemy.getOriginalColor();
    }

    public void heal(float amount)
    {
        health += amount;
        if (health > healthMax)
        {
            health = healthMax;
        }
        healthBar.fillAmount = health / healthMax;
    }
}
