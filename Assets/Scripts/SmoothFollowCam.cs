//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SmoothFollowCam : MonoBehaviour
//{


//    //����ٴ� ������Ʈ�� Transform�� ����
//    public Transform target;
//    // ������Ʈ���� �Ÿ�
//    public float distance = 10.0f;
//    //������Ʈ���� ����
//    public float height = 5.0f;
//    //������Ʈ�� Y�� �̵��� ���󰡴� �ڿ������� �ӵ�
//    public float heightDamping = 2.0f;
//    //������Ʈ�� Y�� ȸ���� ���󰡴� �ڿ������� �ӵ�
//    public float rotationDamping = 0.0f;



//    // �� �����ӿ� ��� Update�� ����� �� ȣ��Ǵ� �Լ���
//    // �ַ� ī�޶��� �̵��̳� Update�� ���� ����Ǿ� �� ������ ���
//    void LateUpdate()
//    {
//        // ���� Ÿ���� ������ ����
//        if (!target)
//            return;

//        // Ÿ���� ���Ϸ� �ޱ۰� Y�� �Ҵ�(���ϴ� ����� ����)
//        float wantedRotationAngle = target.eulerAngles.y;
//        // Ÿ���� Y�� �� height ��ŭ ������ ��ġ�� ���� �Ҵ�(���ϴ� ����� ����) 
//        float wantedHeight = target.position.y + height;

//        //���� ���ӿ�����Ʈ�� ���Ϸ� �ޱ۰� Y�� �Ҵ�
//        float currentRotationAngle = transform.eulerAngles.y;
//        //���� ���ӿ�����Ʈ�� ��ġ ������ Y �� �Ҵ�
//        float currentHeight = transform.position.y;

//        //���� ȸ������ ���ϴ� ȸ�������� �ڿ������� ��ȭ���� 
//        //rotationDamping * Time.deltaTime(����̽� ���� ����) ���ڸ� ����. 
//        //Mathf.LerpAngle�� ������ ���ڴ� 1 �� 100% �̸� 0�� 0%
//        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

//        //ī�޶��� �ڿ������� Y �� ������ ���Ͽ� Mathf.Lerp�� ���
//        //Mathf.Lerp(���� ��,��ǥ ��, ���� ��)
//        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

//        // Quaternion.Euler �Լ��� Vector3 ���� Quaternion ������ ��ȯ���ش�
//        // Transform�� rotation�� Quaternion �� �̴�.
//        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

//        // ������ Transform.position�� �ٷ� ���ԵǾ�����
//        // �ֱٿ��� ������� ���ؼ� ������ ���� �����Ѵ�.

//        //���� Ÿ�� Ʈ�������� �������� ����
//        Vector3 tempDis = target.position;
//        // ���� ���������� ����.. 
//        // ���� �����̼ǿ� Vector3.forward �� (0,0,1) * distance
//        // �� ���� ��(�� �� ����ŭ ������ ũ���� ����) ��ŭ ������ ���͸� ���� 
//        tempDis -= currentRotation * Vector3.forward * distance;

//        // tempDis�� y ���� currentHeight�� ����
//        tempDis.y = currentHeight;
//        //���ӿ�����Ʈ�� Transform ������Ʈ�� position�� tempDis�� ����
//        transform.position = tempDis;

//        // LookAt �Լ��� ���ڷ� ���޵� Transform ���� �����Ͽ� 
//        // ���ü�� ����
//        transform.LookAt(target);
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCam : MonoBehaviour
{
    public Transform target;  // ���� ��� (�÷��̾�)

    public float distance = 8.0f;   // �÷��̾���� �Ÿ� (X-Z ���)
    public float height = 6.0f;     // �÷��̾�� �󸶳� ���� ��ġ�� ������
    public float heightDamping = 2.0f;   // Y�� �̵� �ε巯�� ����

    public float fixedAngleX = 45.0f; // ������ X�� ȸ���� (���ͺ� ����)

    void LateUpdate()
    {
        if (!target)
            return;

        // Ÿ���� Y�� ȸ���� ������ �ʵ��� ���� (������ ������ ���)
        float wantedHeight = target.position.y + height;
        float currentHeight = Mathf.Lerp(transform.position.y, wantedHeight, heightDamping * Time.deltaTime);

        // ī�޶� ��ġ ���� (�÷��̾� �ڰ� �ƴ�, ������ �ٶ󺸴� ���� ����)
        Vector3 targetPosition = target.position - (Quaternion.Euler(fixedAngleX, 0, 0) * Vector3.forward * distance);
        targetPosition.y = currentHeight; // ���� ����
        transform.position = targetPosition;

        // ī�޶�� �׻� Ÿ���� �ٶ�
        transform.LookAt(target);
    }
}
