using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    private int playerDamage;

    public void Start()
    {
        playerDamage = GameObject.Find("Player").GetComponent<PlayerController>().Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            MonsterController enemy = collision.gameObject.GetComponent(typeof(MonsterController)) as MonsterController;
            enemy.TakeDamage(playerDamage);
            Debug.Log("Dealing damage: HP=" + enemy.HealthPoints);
        }
    }
}
