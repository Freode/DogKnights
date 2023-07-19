using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToNextStage : MonoBehaviour
{

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
            Debug.Log("True");
        }
    }
}
