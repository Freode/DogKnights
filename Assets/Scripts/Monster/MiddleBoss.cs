using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss : Monster
{
    [SerializeField]
    private GameObject Fire;
    [SerializeField]
    private Transform FirePort; 

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();   

        StartCoroutine(BossPattern());
    }

    IEnumerator BossPattern()
    {
        yield return new WaitForSeconds(0.1f);

        int randAction = Random.Range(0, 6);
        switch (randAction)
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
                StartCoroutine(TailAttack());
                break;
            case 5:
                // ���鿡�� �һձ�
                StartCoroutine(FireballShoot());
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
        isInvincible = true;
        anim.SetTrigger("doDefend");
        yield return new WaitForSeconds(GetAnimationLength("Defend"));
        isInvincible = false;
        yield return new WaitForSeconds(1f);
        StartCoroutine(BossPattern());
    }
    IEnumerator BasicAttack()
    {
        anim.SetTrigger("doBasicAttack");
        EnabledArea();
        yield return new WaitForSeconds(GetAnimationLength("Basic Attack"));
        attackArea.enabled = false;
        yield return new WaitForSeconds(1f);
        StartCoroutine(BossPattern());
    }
    IEnumerator TailAttack()
    {
        anim.SetTrigger("doTailAttack");
        EnabledArea();
        yield return new WaitForSeconds(GetAnimationLength("Tail Attack"));
        attackArea.enabled = false;
        yield return new WaitForSeconds(1f);
        StartCoroutine(BossPattern());
    }
    IEnumerator FireballShoot()
    {
        anim.SetTrigger("doFireballShoot");
        yield return new WaitForSeconds(0.5f);
        GameObject instantFire = Instantiate(Fire, FirePort.position, FirePort.rotation);
        Rigidbody rigidFire = instantFire.GetComponent<Rigidbody>();   
        rigidFire.velocity = transform.forward * 20;
        Debug.Log(rigidFire.velocity);
        yield return new WaitForSeconds(GetAnimationLength("Fireball Shoot"));
        Destroy(instantFire);
        yield return new WaitForSeconds(1f);
        StartCoroutine(BossPattern());
    }
}
