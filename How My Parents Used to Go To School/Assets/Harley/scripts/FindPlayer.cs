using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;

    private void Awake() {
        var player = GameObject.Find("Male_C");
        this.GetComponent<Enemy>().SetTarget(player, bulletPrefab);
        // this.GetComponent<EnemyStatContainer>().IncreaseStats(scale);
        // this.GetComponent<EnemyStatContainer>().IncreaseStats(PlayerPrefs.GetFloat("Scale"));
    }
}
