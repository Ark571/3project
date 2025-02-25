using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float rotationSpeed = 10f; // 회전 속도
    private Rigidbody rb;
    private Vector3 moveInput;
    private Animator anim; // 애니메이터 추가

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 가져오기
        anim = GetComponent<Animator>(); // Animator 가져오기
    }

    void Update()
    {
        // 입력 감지 (WASD / 방향키)
        float moveX = Input.GetAxisRaw("Horizontal"); // 좌우 (A, D)
        float moveZ = Input.GetAxisRaw("Vertical");   // 앞뒤 (W, S)

        // 이동 벡터 계산 (Y는 고정)
        moveInput = new Vector3(moveX, 0, moveZ).normalized;

        //이동속도 전달
        anim.SetFloat("MoveSpeed", moveInput.magnitude * moveSpeed);

        //// 이동 중이면 true, 멈추면 false
        //anim.SetBool("Move", moveInput != Vector3.zero);

        //좌클릭 시 펀치 트리거 실행
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Punch");
        }

        // 캐릭터가 움직일 때만 방향을 변경
        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Y축은 유지하고 이동 적용
        rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.z * moveSpeed);
    }
}
