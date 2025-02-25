using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    public int damage = 10; // 공격 데미지

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster")) // Monster 태그를 가진 오브젝트와 충돌하면
        {
            // 몬스터가 Damageable 스크립트를 가지고 있다면 데미지를 입힘
            MonsterHp monster = other.GetComponent<MonsterHp>();
            if (monster != null)
            {
                monster.TakeDamage(damage);
            }
        }
    }
}
