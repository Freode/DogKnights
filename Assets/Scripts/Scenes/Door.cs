using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // �� �ǹ� ����.
    public GameObject door_mesh_infor;

    // Start is called before the first frame update
    void Start()
    {
        // GameObject.FindGameObjectWithTag()
        // �� ������ �ش� ������Ʈ�� �±׸� ���� ù ��° ��ü�� ��ȯ��.

        // GetComponents�� MonoBehavior�� Component Ŭ������ ��ӹ��� �� ����� �� ����. �׷���, GameObject�� �� Ŭ������ ��ӹ��� �����Ƿ� ����� �� ����.

        string tag_name = "";

        //foreach(GameObject temp_object in GetComponents<GameObject>())
        //{
        //    tag_name = temp_object.tag;

        //    if(tag_name.Equals("Pivot"))
        //    {
        //        door_mesh_infor = temp_object;
        //        Debug.Log(temp_object);
        //        break;
                
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
