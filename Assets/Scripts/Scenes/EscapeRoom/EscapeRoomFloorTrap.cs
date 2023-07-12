using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeRoomFloorTrap : MonoBehaviour
{
    // ===== public =====

    // Ʈ���� ����/�ö���� �ּ� �ð� 
    public float minMoveTimes = 0.3f;

    // Ʈ���� ����/�ö���� �ִ� �ð�
    public float maxMoveTimes = 0.9f;

    // Ʈ�� �ּ� �ẹ �ð�
    public float minHiddenTimes = 1.5f;

    // Ʈ�� �ִ� �ẹ �ð�
    public float maxHiddenTimes = 4.0f;

    // Ʈ�� �ּ� ���� �ð�
    public float minProtrusionTimes = 1.2f;

    // Ʈ�� �ִ� ���� �ð�
    public float maxPortrusionTimes = 2.25f;

    // ===== private =====

    // Ʈ�� �ẹ �ð� ( �ּ� �ẹ �ð��� �ִ� �ẹ �ð� ���̿��� ���������� ����. )
    private float hiddenTimes = 0f;

    // Ʈ�� ���� �ð� ( �ּ� ���� �ð��� �ִ� ���� �ð� ���̿��� ���������� ����. )
    private float protrusionTimes = 0f;

    // Ʈ�� �̵� �ð� ( �ּ� �̵� �ð��� �ִ� �̵� �ð� ���̿��� ���������� ����. )
    private float moveTimes = 0f;

    // ���� �ð�
    private float maintainenceTime = 0f;

    // ���� ����/�ö���� �� ��� �������� Ȯ��
    private bool bIsLiftingOff = false;

    // ���� ���¸� �����ϴ��� Ȯ���ϴ� ����
    private bool bIsMaintainence = false;

    // ���� Ʈ�� ����
    private float height = 0.3f;

    // �ӽ� ��ġ ����
    private Vector3 tempLocation;

    private Rigidbody r;

    // Start is called before the first frame update
    void Start()
    {
        // ���������� �ʱ�ȭ
        hiddenTimes = Random.Range(minHiddenTimes, maxHiddenTimes);
        protrusionTimes = Random.Range(minProtrusionTimes, maxPortrusionTimes);
        moveTimes = Random.Range(minMoveTimes, maxMoveTimes);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(height);
        //Debug.Log(transform.position);
        // ���� ���� ������ ��
        if (bIsMaintainence)
        {
            // ���� �ð����� �� ��
            if((bIsLiftingOff ? protrusionTimes : hiddenTimes) > maintainenceTime)
            {
                // ���� ���� ����
                bIsMaintainence = false;

                // ���� ���� �ð� �ʱ�ȭ
                maintainenceTime = 0f;

                // ���� ������ �ݴ�� ����.
                bIsLiftingOff = !bIsLiftingOff;
            }

            else
            {
                maintainenceTime += Time.deltaTime;
            }

        }

        // ���� ���� ������ �ƴ� ��
        else
        {
            // ���� �ö���� ���̶��
            if(bIsLiftingOff)
            {
                // Ʈ���� �ִ� ���̱��� �ö�Դٸ�
                if (height > 0.3f)
                {
                    // ���� ����
                    height = 0.3f;

                    // ���� ���·� ����
                    bIsMaintainence = true;
                }
                // Ʈ���� �ִ� ���̱��� �ö���� �ʾҴٸ�
                else
                {
                    // ���� �ø���
                    height += 0.3f / moveTimes * Time.deltaTime;
                }
            }

            // ���� ���� ���̶��
            else
            {
                // Ʈ���� �ִ� ���̱��� �ö�Դٸ�
                if (height < -0.3f)
                {
                    // ���� ����
                    height = -0.3f;

                    // �� ���·� ����
                    bIsMaintainence = true;
                }
                // Ʈ���� �ּ� ���̱��� �������� �ʾҴٸ�
                else
                {
                    // ���� ������
                    height -= 0.3f / moveTimes * Time.deltaTime;
                }
                // ��ġ ����

                //transform.position = new Vector3(transform.position.x, height, transform.position.z);
            }

            // ��ġ ����
            tempLocation = transform.position;
            tempLocation.y = height;
            transform.position = tempLocation;
        }
    }
}
