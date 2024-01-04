using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : NextStageDoor
{

    // ===== public =====

    // ���� ������ �ӵ�
    public float door_rotation_speed = 70f;

    // ���� ������ ����
    public float door_min_rotation_angle = 0f;

    // ���� �ִ�� ������ ����
    public float door_max_rotation_angle = 90f;

    // �� ���� ���� ���� ����
    public bool b_is_door_closed = true;

    // ���� �� ����
    public GameObject left_door;

    // ������ �� ����
    public GameObject right_door;

    // ���� �������� ��ȣ
    public int current_stage;

    // ���� �������� ��ȣ
    public int next_stage;

    // ===== private =====

    // ���� ���� ���� ����
    private float current_angle_door_y = 0f;

    // ���� ���� ������ �ִϸ��̼� �۵� ����
    private bool b_is_door_opening = false;

    // ���� ���������� �̵��ϴ� Box Collider
    private BoxCollider portalCollider;


    void Awake()
    {
        // ���� ���� ������ ���� ���������� ����� ���� ����.
        if(left_door != null && right_door != null)
        {
            // ���� ���� �ִٸ�
            if(b_is_door_closed)
            {
                // �� ȸ���� �ʱ�ȭ
                left_door.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                right_door.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

                // ���� ȸ�� ���� ����
                current_angle_door_y = 0f;
            }

            // ���� ���� �ִٸ�
            else
            {
                // �� ȸ���� �ʱ�ȭ
                left_door.transform.localEulerAngles = new Vector3(0f, -door_max_rotation_angle, 0f);
                right_door.transform.localEulerAngles = new Vector3(0f, door_max_rotation_angle, 0f);

                // ���� ȸ�� ���� ����.
                current_angle_door_y = door_max_rotation_angle;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        portalCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // �� ���� �۵� ��
        if(b_is_door_opening)
        {
            // ���� ������ ���� ���
            if(b_is_door_closed)
            {
                // �� ȸ�� ���� ����
                current_angle_door_y += door_rotation_speed * Time.deltaTime;

                // ���� ���� ������ �ִ� ȸ�� �������� Ŭ ��,
                if(current_angle_door_y >= door_max_rotation_angle)
                {
                    // �ִ� ȸ�� ������ ����
                    current_angle_door_y = door_max_rotation_angle;

                    // �� ���� ����
                    b_is_door_opening = false;

                    // ���� ������ �������� �˸�
                    b_is_door_closed = false;
                }
            }

            // ���� ������ ���� ���
            else
            {
                // �� ȸ�� ���� ����
                current_angle_door_y -= door_rotation_speed * Time.deltaTime;

                // ���� ���� ������ �ּ� ȸ�� �������� ���� ��,
                if (current_angle_door_y < door_min_rotation_angle)
                {
                    // �ִ� ȸ�� ������ ����
                    current_angle_door_y = door_min_rotation_angle;

                    // �� ���� ����
                    b_is_door_opening = false;

                    // ���� ������ �������� �˸�
                    b_is_door_closed = true;
                }
            }

            // �� ȸ�� ����.
            left_door.transform.localEulerAngles = new Vector3(0f, -current_angle_door_y, 0f);
            right_door.transform.localEulerAngles = new Vector3(0f, current_angle_door_y, 0f);

        }
    }
    /// <summary>
    /// �� ���ݴ� �۵� ����.
    /// </summary>
    /// <param name="b_is_door_opening">�� �۵� ����(Default = true)</param>
    void DoorOperate(bool b_is_door_opening = true)
    {
        this.b_is_door_opening = b_is_door_opening;
    }

    /// <summary>
    /// ���� ���������� �̵�
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Main Moon Collision!");
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<FloorManager>().NextStage(collision.gameObject, current_stage, next_stage);
        }
    }

    // �� ���� ������ ����
    public override void PrepareNextStageDoorOpened()
    {
        base.PrepareNextStageDoorOpened();
        DoorOperate(true);
    }
}
