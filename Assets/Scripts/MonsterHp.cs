using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour
{
    public int health = 50; // 몬스터의 체력

    private void Awake()
    {
        
    }


    public void TakeDamage(int damage)
    {

        health -= damage;
        Debug.Log(gameObject.name + " 체력: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " 사망!");
        Destroy(gameObject); // 몬스터 오브젝트 제거
    }
}
