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

    [Tooltip("�����ִ� �ð� ����")]
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
            //�̵� ����
            isMoving = true;
            ChangeDirection();
            UpdateAnimator(1f);

            // ���� �ð� ���� �̵�
            float MoveDuration = Random.Range(2f, 4f);
            float moveTimer = 0f;
            while(moveTimer < MoveDuration)
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
                RotateTowardsMovement();
                moveTimer += Time.deltaTime;
                yield return null;
            }

            // 3. ���߱� (WaitTime �� ����)
            isMoving = false;
            UpdateAnimator(0f); // �̵� �ִϸ��̼� ��Ȱ��ȭ
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
            animator.SetFloat("Speed", speed * moveSpeed); // Speed �Ķ���� �� ����
        }
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
