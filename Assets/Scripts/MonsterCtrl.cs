using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterCtrl : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;

    public int health = 70;

    [Tooltip("멈춰있는 시간 범위")]
    [Range(1f, 5f)]
    [SerializeField] float WaitTime = 5f;

    private Vector3 moveDirection;
    private bool isMoving = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        StartCoroutine(MoveRoutine());
    }
    
    IEnumerator MoveRoutine()
    {
        while (true)
        {
            //이동 시작
            isMoving = true;
            ChangeDirection();
            UpdateAnimator(1f);

            // 일정 시간 동안 이동
            float MoveDuration = Random.Range(2f, 4f);
            float moveTimer = 0f;
            while(moveTimer < MoveDuration)
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
                RotateTowardsMovement();
                moveTimer += Time.deltaTime;
                yield return null;
            }

            // 3. 멈추기 (WaitTime 초 동안)
            isMoving = false;
            UpdateAnimator(0f); // 이동 애니메이션 비활성화
            yield return new WaitForSeconds(WaitTime);
        }
    }

    void ChangeDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    void RotateTowardsMovement()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void UpdateAnimator(float speed)
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", speed * moveSpeed); // Speed 파라미터 값 변경
        }
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
