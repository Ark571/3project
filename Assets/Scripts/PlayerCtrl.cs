using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float rotationSpeed = 10f; // ȸ�� �ӵ�
    private Rigidbody rb;
    private Vector3 moveInput;
    private Animator anim; // �ִϸ����� �߰�

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ��������
        anim = GetComponent<Animator>(); // Animator ��������
    }

    void Update()
    {
        // �Է� ���� (WASD / ����Ű)
        float moveX = Input.GetAxisRaw("Horizontal"); // �¿� (A, D)
        float moveZ = Input.GetAxisRaw("Vertical");   // �յ� (W, S)

        // �̵� ���� ��� (Y�� ����)
        moveInput = new Vector3(moveX, 0, moveZ).normalized;

        //�̵��ӵ� ����
        anim.SetFloat("MoveSpeed", moveInput.magnitude * moveSpeed);

        //// �̵� ���̸� true, ���߸� false
        //anim.SetBool("Move", moveInput != Vector3.zero);

        //��Ŭ�� �� ��ġ Ʈ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Punch");
        }

        // ĳ���Ͱ� ������ ���� ������ ����
        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Y���� �����ϰ� �̵� ����
        rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.z * moveSpeed);
    }
}
