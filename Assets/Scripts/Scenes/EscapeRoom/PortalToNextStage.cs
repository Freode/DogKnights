using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToNextStage : NextStageDoor
{

    // ===== public =====
    // ���� ��������
    public int current_stage = 0;

    // ���� ��������
    public int next_stage = 0;

    // ===== private =====
    // ��Ż�� Ȱ��ȭ�ƴ����� ����
    bool bIsPortalOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ��Ż ����Ʈ �� �浹 Ȱ��ȭ
    /// </summary>
    public void PortalActivate()
    {
        ParticleSystem particleSystem = transform.Find("FX").gameObject.GetComponent<ParticleSystem>();
        bIsPortalOn = true;
        particleSystem.Play();
    }

    // �÷��̾�� �浹���� ��
    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾� �±װ� �ε������� ��Ż�� Ȱ��ȭ�Ǿ��� ���� �۵�.
        if(collision.gameObject.CompareTag("Player") && bIsPortalOn)
        {
            // ���� ���������� �̵�.
            FindObjectOfType<FloorManager>().NextStage(collision.gameObject, current_stage, next_stage);
            // Debug.Log("True");
            if(current_stage==4)
            {
                current_stage = 9;
                next_stage = 10;
            }
        }
    }

    // ��Ż Ȱ��ȭ
    public override void PrepareNextStageDoorOpened()
    {
        base.PrepareNextStageDoorOpened();
        gameObject.SetActive(true);
    }
}
