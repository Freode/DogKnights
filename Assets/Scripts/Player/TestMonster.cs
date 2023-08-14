using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMonster : MonoBehaviour
{
    [SerializeField]
    private enum Type { Melee, Charge, Ranged };
    [SerializeField]
    private Type monsterType;
    [SerializeField]
    private int damage;

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float curHealth;
    [SerializeField]
    private BoxCollider monsterAttack; //���ݹ���
    [SerializeField]
    private Transform target; //����Ÿ��
    [SerializeField]
    private bool isChase; //�������ΰ�
    [SerializeField]
    private bool isAttack; //�������ΰ�


    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;

    private float curHitDis;
    private float targetRadius = 0f; //��
    private float targetRange = 0f; //���ݹ���

    public int Damage
    {
        get { return damage; }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        Invoke("ChaseStart", 2); //2�� �� ����
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        //�׺���̼� Ȱ��ȭ�Ǿ� �������� ����
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase; //���߱�
        }
            
    }

    //�÷��̾�� �������� �浹�Ͼ ��
    //������ٵ� velocity �������� �߰��Ǿ��ֱ� ������
    //�浹�ϸ� �����ӵ��� ���� �����ӿ� ��ȭ�� ����
    //velocity�� ��� �����Ǿ� �ֱ� ������ �������ϴ� ���°� �Ǿ� �����ϰ�����
    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void Targeting()
    {
        switch (monsterType)
        {
            case Type.Melee:
                targetRadius = 0.6f;
                targetRange = 0.6f;
                break;
            case Type.Charge:
                targetRadius = 0.4f;
                targetRange = 12f;
                break;
            case Type.Ranged:
                targetRadius = 0.5f;
                targetRange = 25f;
                break;
        }

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position,
                                  targetRadius, 
                                  transform.forward,
                                  targetRange, //���� ����(�Ÿ�)
                                  LayerMask.GetMask("Player"));
        curHitDis = 0;
        foreach (RaycastHit hit in rayHits)
        {
            curHitDis = hit.distance;
        }
        //�ϳ��� �ɸ��� && �������϶� �װŸ��������� �����ؾ���
        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.forward * curHitDis);
        Gizmos.DrawWireSphere(transform.position + transform.position * curHitDis, targetRadius);
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        
        switch (monsterType)
        {
            case Type.Melee:
                yield return new WaitForSeconds(0.2f);
                monsterAttack.enabled = true; //���ݹ��� Ȱ��ȭ
                anim.SetBool("isAttack", true);

                yield return new WaitForSeconds(1f);
                monsterAttack.enabled = false;
                anim.SetBool("isAttack", false);

                yield return new WaitForSeconds(1f);
                break;
            case Type.Charge:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
                monsterAttack.enabled = true;

                yield return new WaitForSeconds(1f);
                rigid.velocity = Vector3.zero;
                monsterAttack.enabled = false;

                yield return new WaitForSeconds(2f);
                break;
            case Type.Ranged:

                break;
        }
        isChase = true;
        isAttack = false;
    }

    // �����ð����� �����ϱ� ������ ����ó���� �� �� ���
    void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sword")
        {
            Sword sword = other.GetComponent<Sword>();
            curHealth -= sword.damage;
            Vector3 reactVec = transform.position - other.transform.position; //�ӹ�(���ۿ�) : ���� ��ġ - �ǰ� ��ġ
            Debug.Log("Sword : " + curHealth);
            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0)
        {
            mat.color = Color.white;
        }
        else //���
        {
            mat.color = Color.gray;
            gameObject.layer = 9; //MonsterDead
            isChase = false; //��������� �����ߴ�
            nav.enabled = false; //NavAgent ��Ȱ��ȭ(�ӹ� ���׼��� �츮�����ؼ�)

            anim.SetTrigger("doDie");

            //�ӹ�
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);

            Destroy(gameObject, 4); //4�� �� ����
        }
    }
}
