using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeDoor : MonoBehaviour
{
    // ===== public =====

    // �� ȸ�� �ӵ�
    public float door_rotation_speed = 70f;

    // �� �ּ� ȸ�� ����
    public float door_min_rotation_angle = 0f;

    // �� �ִ� ȸ�� ����
    public float door_max_rotation_angle = 130f;

    // �� ȸ�� ����
    public bool b_is_clockwise = true;

    // ===== private =====

    // �� �ǹ� ����.
    private GameObject door_mesh_infor;

    // ���� ���� �ִϸ��̼� ��� ����
    private bool b_is_door_opened = false;

    // �� Game Object ��ü ����
    private GameObject door;
    
    // ���� �� ����.
    private float door_rotation_y = 0f;

    //  public ���� ����, private���� ������ ��, Serialize�� Ȱ��.

    // Start is called before the first frame update
    void Start()
    {
        // GameObject.FindGameObjectWithTag()
        // �� ������ �ش� ������Ʈ�� �±׸� ���� ù ��° ��ü�� ��ȯ��.

        // GetComponents�� MonoBehavior�� Component Ŭ������ ��ӹ��� �� ����� �� ����. �׷���, GameObject�� �� Ŭ������ ��ӹ��� �����Ƿ� ����� �� ����.

        // GameObject.Find() �Լ��� Scene�� ��ġ�Ǿ� �ִ� ��� ��ü�� ���ؼ� �˻縦 ������. ( ���� )

        // transform.Find() �Լ��� ���� ��ũ��Ʈ�� �ִ� ������Ʈ�� �ڽ� Game Object�� ���ؼ��� �˻縦 ������. ( ���� )

        // transform.Find() �Լ����� �Ķ������ �Է°����� ���ٸ� ��ξ��� ���ڿ��� �Է�������, �ش� Game Object�� �ڽĿ� ���ؼ��� �˻縦 ������. "~/~"�� �Ķ���ͷ� �Է��� ���, �ڽ��� �ڽĿ� ���ؼ��� �˻���.

        // 'door_pivot' Game Object ��ü ������ ������.
        door_mesh_infor = transform.Find("door_pivot").gameObject;

        // 'door' Game Object ������ ������.
        door = transform.Find("door_pivot/door").gameObject;

        // 'door' Game Object�� Y�� ȸ������ ����.
        // transform.eulerAngles.y ���� �б⸸ �����ϰ� ����� �Ұ�����.
        door_rotation_y = door.transform.localEulerAngles.y;

        DoorOperate();
    }

    // Update is called once per frame
    void Update()
    {

        // �� ���� �ִϸ��̼� ���
        if(b_is_door_opened)
        {
            // ���� ���� ����.
            if(b_is_clockwise)
            {
                door_rotation_y += door_rotation_speed * Time.deltaTime;
            }

            // ���� �ݴ� ����.
            else
            {
                door_rotation_y -= door_rotation_speed * Time.deltaTime;
            }

            // ���� ������ ������ ��
            if(door_rotation_y <= door_min_rotation_angle)
            {
                // ���� �ּ� ������ ����.
                door_rotation_y = door_min_rotation_angle;

                // ���� �� �̻� �������� �ʵ��� ����.
                // b_is_door_opened = false;

                // ���� ������ �������� ����.
                b_is_clockwise = true;
            }

            // ���� ������ ������ ��
            else if(door_rotation_y >= door_max_rotation_angle)
            {
                // ���� �ִ� ������ ����.
                door_rotation_y = door_max_rotation_angle;

                // ���� �� �̻� �������� �ʵ��� ����.
                // b_is_door_opened = false;

                // ���� ������ �������� ����.
                b_is_clockwise = false;
            }

            // ���� ���� �����ų� ������ �ִ� ���߿�
            else
            {
                // Ignore
            }

            // �� ȸ������ ����.
            door.transform.localRotation = Quaternion.Euler(0f, door_rotation_y, 0f);
        }
    }

    // ���� �۵���Ű�� �Լ�.
    public void DoorOperate()
    {
        b_is_door_opened = true;
    }
}
