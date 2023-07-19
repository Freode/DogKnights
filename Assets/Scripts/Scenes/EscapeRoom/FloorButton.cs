using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
    // ===== public =====

    [Tooltip("��ư�� renderer")]
    public Renderer buttonRender;

    // ===== private =====

    // ��ư ������
    private FloorButtonsManager floorButtonsManager;

    // ��ȣ�ۿ��� �� ���� �ϵ��� �����ϱ� ���� ����.
    private bool bIsOnce = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ��ư ������ ����
    /// </summary>
    /// <param name="floorButtonsManager_">�ش� ��ư�� ���� ��ư ������ ��ü</param>
    public void SetFloorButtonsManager(FloorButtonsManager floorButtonsManager_)
    {
        this.floorButtonsManager = floorButtonsManager_;
    }

    // �÷��̾���� �浹 ����
    private void OnCollisionEnter(Collision collision)
    {
        // �浹ü�� �±װ� "Player"�̰� �� ���� �۵��ϵ��� ����.
        if (collision.gameObject.CompareTag("Player") && bIsOnce)
        {
            // �� �̻� ��ȣ�ۿ����� ���ϵ��� ����
            bIsOnce = false;

            // û�ϻ����� ����
            buttonRender.material.color = Color.cyan;
            buttonRender.material.SetColor("_EmissionColor", Color.cyan);

            // ��ȣ�ۿ� ��ư ���� ����
            floorButtonsManager.ButtonCollisionCountIncrease();
        }
    }
}
