using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [Tooltip("�ش� ������ �۵��ؾ��� ��ü��")]
    public FloorController[] objects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �۵��ؾ��� ��ü ����
    /// </summary>
    void Operate()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].OperateObjects();
        }
    }
}
