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

    int currentPhase = 1; // ���� �ʱ� ������

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>();
        anim = GetComponentInChildren<Animator>();
        isInvincible = true;
    }


    void Update()
    {
        if (isDead)
        {
            // ���� ���°� ����� �� ��� �ڷ�ƾ ����
            StopAllCoroutines();
        }
    }

    public void StartBossPattern()
    {
        isInvincible = false;
        StartCoroutine(BossPattern());
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
        isInvincible = true;
        anim.SetTrigger("doDefend");
        yield return new WaitForSeconds(GetAnimationLength("Defend"));
        isInvincible = false;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(BossPattern());
    }
    IEnumerator BasicAttack()
    {
        anim.SetTrigger("doBasicAttack");
        EnabledArea();
        yield return new WaitForSeconds(GetAnimationLength("Basic Attack"));
        attackArea.enabled = false;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(BossPattern());
    }
    IEnumerator ClawAttack()
    {
        anim.SetTrigger("doClawAttack");
        EnabledArea();
        yield return new WaitForSeconds(GetAnimationLength("Claw Attack"));
        attackArea.enabled = false;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(BossPattern());
    }
    IEnumerator FlameAttack()
    {
        anim.SetTrigger("doFlameAttack");
        yield return new WaitForSeconds(0.5f);
        GameObject instantFire = Instantiate(Fire, FirePort.position, FirePort.rotation);
        yield return new WaitForSeconds(GetAnimationLength("Flame Attack"));
        Destroy(instantFire);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(BossPattern());
    }
    IEnumerator FlyFlameAttack()
    {
        isInvincible = true;
        anim.SetTrigger("doFlyFlameAttack");
        yield return new WaitForSeconds(GetAnimationLength("Take Off"));
        yield return new WaitForSeconds(GetAnimationLength("Fly Float"));
        GameObject instantFire = Instantiate(Fire, FlyFirePort.position, FlyFirePort.rotation);
        yield return new WaitForSeconds(GetAnimationLength("Fly Flame Attack"));
        Destroy(instantFire);
        yield return new WaitForSeconds(GetAnimationLength("Land"));
        isInvincible = false;
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(BossPattern());
    }

    // �ִϸ��̼� �̸��� �޾� �ش� �ִϸ��̼��� ���̸� ��ȯ�ϴ� �Լ�
    float GetAnimationLength(string animationName)
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {  
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0f; // �ִϸ��̼��� ã�� ���� ��� 0�� ��ȯ�ϰų� ���� ó��
    }
}
