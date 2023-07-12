using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonsManager : MonoBehaviour
{
    // ===== public =====
    [Tooltip("��ư ��ü��")]
    public FloorButton[] buttons;


    // ===== private =====

    // ���� ��ȣ�ۿ� ��ư ����
    private int counts = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(FloorButton tempButton in buttons)
        {
            tempButton.SetFloorButtonsManager(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Tooltip("��ư�� �÷��̾��� ��ȣ�ۿ��� �߻����� ��, ��ȣ�ۿ��� ��ư�� �� ������ ������Ų��.")]
    public void ButtonCollisionCountIncrease()
    {
        counts++;
        Debug.Log(counts);
    }
}
