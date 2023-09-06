using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    protected enum Type { Melee, Charge, Ranged, Boss };
    [SerializeField]
    protected Type monsterType;
    [SerializeField]
    protected float maxHealth;
    [SerializeField]
    protected float curHealth;
    [SerializeField]
    protected BoxCollider attackArea; //���ݹ���
    [SerializeField]
    private GameObject rock;
    [SerializeField]
    private Transform target; //����Ÿ��


    protected bool isChase;  //�������ΰ�
    protected bool isAttack; //�������ΰ�
    protected bool isDead;   //�׾��°�
    protected bool isInvincible;   //����(����ߴ���, ���Ҵٴ���)


    protected Rigidbody rigid;
    protected BoxCollider boxCollider;
    protected SkinnedMeshRenderer mat;
    protected NavMeshAgent nav;
    protected Animator anim;

    protected float curHitDis;
    protected float targetRadius = 0f; //��
    protected float targetRange = 0f; //���ݹ���

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        if(monsterType != Type.Boss)
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
        if (nav.enabled && monsterType != Type.Boss)
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
        if (!isDead && monsterType != Type.Boss)
        {
            switch (monsterType)
            {
                case Type.Melee:
                    targetRadius = 0.6f;
                    targetRange = 0.6f;
                    break;
                case Type.Charge:
                    targetRadius = 0.4f;
                    targetRange = 4f;
                    break;
                case Type.Ranged:
                    targetRadius = 0.6f;
                    targetRange = 6f;
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

    }

    void OnDrawGizmosSelected()
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
                attackArea.enabled = true; //���ݹ��� Ȱ��ȭ
                anim.SetBool("isAttack", true);

                yield return new WaitForSeconds(1f);
                attackArea.enabled = false;
                anim.SetBool("isAttack", false);

                yield return new WaitForSeconds(1f);
                break;
            case Type.Charge:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 10, ForceMode.Impulse);
                attackArea.enabled = true;
                anim.SetBool("isAttack", true);

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                attackArea.enabled = false;
                anim.SetBool("isAttack", false);

                yield return new WaitForSeconds(2f);
                break;
            case Type.Ranged:
                anim.SetBool("isAttack", true);
                yield return new WaitForSeconds(1.6f);
                Vector3 pos = new Vector3(transform.position.x + 2.2f, transform.position.y + 1f, transform.position.z);
                GameObject instantRock = Instantiate(rock, pos, rock.transform.rotation);
                Rigidbody rigidRock = instantRock.GetComponent<Rigidbody>();
                rigidRock.velocity = transform.forward * 20;

                yield return new WaitForSeconds(0.4f);
                anim.SetBool("isAttack", false);

                yield return new WaitForSeconds(2f);
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
        if (other.tag == "Sword" && isInvincible == false)
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
        mat.material.color = Color.red;

        //�ӹ�
        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        rigid.AddForce(reactVec * 5, ForceMode.Impulse);

        anim.SetTrigger("getHit");

        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0)
        {
            mat.material.color = Color.white;
        }
        else //���
        {
            mat.material.color = Color.gray;
            gameObject.layer = 9; //MonsterDead
            isDead = true; 
            isChase = false; //��������� �����ߴ�

            anim.SetTrigger("doDie");

            nav.enabled = false; //NavAgent ��Ȱ��ȭ(�ӹ� ���׼��� �츮�����ؼ�)  
            
            if (monsterType != Type.Boss)
                Destroy(gameObject, 4); //4�� �� ����
            
        }
    }
}
