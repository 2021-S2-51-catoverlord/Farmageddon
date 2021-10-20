using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public PlayerController Player;

    public void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            MonsterController enemy = collision.gameObject.GetComponent(typeof(MonsterController)) as MonsterController;
            enemy.TakeDamage(Player.Damage);
            Debug.Log("Dealing damage: HP =" + enemy.HealthPoints);
        }
    }
}
