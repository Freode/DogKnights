using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
    [SerializeField]
    private GameObject Fire;
    [SerializeField]
    private Transform FirePort;
    [SerializeField]
    private Transform FlyFirePort;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();

        StartCoroutine(BossPattern());
    }


    void Update()
    {
        if (isDead)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator BossPattern()
    {
        yield return new WaitForSeconds(0.1f);

        int randAction = Random.Range(0, 7);
        switch(randAction)
        {
            case 0:
                // ���
                StartCoroutine(Defend());
                break;
            case 1:
            case 2:
                // 1Ÿ ���� ����
                StartCoroutine(BasicAttack());
                break;
            case 3:
            case 4:
                // 2Ÿ ���� ����
                StartCoroutine(ClawAttack());
                break;
            case 5:
                // ���鿡�� �һձ�
                StartCoroutine(FlameAttack());
                break;
            case 6:
                // ���Ƽ� �һձ�
                StartCoroutine(FlyFlameAttack());
                break;
        }
    }

    void EnabledArea()
    {
        targetRadius = 5.0f;
        targetRange = 5.0f;

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position,
                                  targetRadius,
                                  transform.forward,
                                  targetRange, //���� ����(�Ÿ�)
                                  LayerMask.GetMask("Player"));

        //�ϳ��� �ɸ��� && �������϶� �װŸ��������� �����ؾ���
        if (rayHits.Length > 0 && !isAttack)
        {
            attackArea.enabled = true; //���ݹ��� Ȱ��ȭ
        }
    }

    IEnumerator Defend()
    {
        anim.SetTrigger("doDefend");
        isInvincible = true;
        yield return new WaitForSeconds(2f);
        isInvincible = false;
        yield return new WaitForSeconds(2f);
        StartCoroutine(BossPattern());
    }
    IEnumerator BasicAttack()
    {
        anim.SetTrigger("doBasicAttack");
        yield return new WaitForSeconds(0.2f);
        EnabledArea();
        yield return new WaitForSeconds(1f);
        attackArea.enabled = false;
        yield return new WaitForSeconds(3f);
        StartCoroutine(BossPattern());
    }
    IEnumerator ClawAttack()
    {
        anim.SetTrigger("doClawAttack");
        yield return new WaitForSeconds(0.2f);
        EnabledArea();
        yield return new WaitForSeconds(1.4f);
        attackArea.enabled = false;
        yield return new WaitForSeconds(3f);
        StartCoroutine(BossPattern());
    }
    IEnumerator FlameAttack()
    {
        anim.SetTrigger("doFlameAttack");
        yield return new WaitForSeconds(0.5f);
        GameObject instantFire = Instantiate(Fire, FirePort.position, FirePort.rotation);
        yield return new WaitForSeconds(1.5f);
        Destroy(instantFire);
        yield return new WaitForSeconds(3.4f);
        StartCoroutine(BossPattern());
    }
    IEnumerator FlyFlameAttack()
    {
        isInvincible = true;
        anim.SetTrigger("doFlyFlameAttack");
        yield return new WaitForSeconds(4.2f);
        GameObject instantFire = Instantiate(Fire, FlyFirePort.position, FlyFirePort.rotation);
        yield return new WaitForSeconds(1.8f);
        Destroy(instantFire);
        yield return new WaitForSeconds(4.2f);
        isInvincible = false;
        yield return new WaitForSeconds(2f);
        StartCoroutine(BossPattern());
    }
}
