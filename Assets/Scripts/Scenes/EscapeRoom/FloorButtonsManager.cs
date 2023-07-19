using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonsManager : MonoBehaviour
{
    // ===== public =====
    [Tooltip("��ư ��ü��")]
    public FloorButton[] buttons;


    [Tooltip("���� ������ �̵��� �� �ִ� ��Ż��")]
    public PortalToNextStage[] portals;

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

    /// <summary>
    /// ��ư�� �÷��̾��� ��ȣ�ۿ��� �߻����� ��, ��ȣ�ۿ��� ��ư�� �� ������ ������Ų��.
    /// </summary>
    public void ButtonCollisionCountIncrease()
    {
        counts++;
        // ���� ��ư ��ü ���� UI�� ���� �ʿ�.

        // ����Ʈ�� �ִ� ��ư �� ������ ���� ��ư ���� ��ġ.
        if(counts == 1)
        {
            // ���� ���������� �̵��� ��Ż ��ȣ ����.
            int openPortalNumber = Random.Range(0, portals.Length);

            portals[openPortalNumber].PortalActivate();
        }
    }
}
