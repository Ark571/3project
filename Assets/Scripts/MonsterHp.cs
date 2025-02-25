using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour
{
    public int health = 50; // ������ ü��

    private void Awake()
    {
        
    }


    public void TakeDamage(int damage)
    {

        health -= damage;
        Debug.Log(gameObject.name + " ü��: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ���!");
        Destroy(gameObject); // ���� ������Ʈ ����
    }
}
