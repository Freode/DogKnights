using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private Transform player; // �÷��̾��� ��ġ
    private bool isFlipped = false; // ������ ���� ������ �ִ���

    [SerializeField]
    private int maxHP = 300;
    [SerializeField]
    private int attackDamage = 1;
    [SerializeField]
    private int FlameAttackDamage = 2;
    [SerializeField]
    private int FlyFlameAttackDamge = 2;

    [SerializeField]
    private Vector3 attackOffset;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private LayerMask attackMask;

    private GameObject deathEffect;
    public bool isInvulnerable = false; // ����

    public void LookAtPlayer() // ������ �÷��̾ �ٶ󺸰� �ϴ� ���
    {
        Vector3 flipped = transform.localScale; 
        flipped.x *= -1f; // x�� ����

        // ������ �÷��̾�� x�� �������� �� �����ʿ� �ְ�,
        // ������ �̹� ������ �ִٸ�(�÷��̾ �ٶ󺸰� �ִٸ�)
        // ������ �ٽ� ���� �������� ����
        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        // ������ �÷��̾�� x�� �������� �� ���ʿ� �ְ�,
        // ������ ���� �������� �ʾҴٸ�(�÷��̾ ������ �ִٸ�)
        // ������ ����� �÷��̾ �ٶ󺸰� ��
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    // ����� ����
    public void Attack()
    {
        Debug.Log("Attack");
        Vector3 pos = transform.position;
        // ���� ����
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        // ���� ���� �� ��ü Ž��
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        // ���� ���� �� ��ü�� ������
        if(colInfo != null)
        {         
            colInfo.GetComponent<Player>().TakeDamage(attackDamage);
            Debug.Log("attackDamage : 1");
        }
    }
    public void FlameAttack()
    {
        Debug.Log("FlameAttack");
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player>().TakeDamage(FlameAttackDamage);
            Debug.Log("FlameAttackDamage : 2");
        }
    }
    public void FlyFlameAttack()
    {
        Debug.Log("FlameAttack");
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player>().TakeDamage(FlyFlameAttackDamge);
            Debug.Log("FlyFlameAttackDamge : 2");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        maxHP -= damage;
        Debug.Log("MaxHP : 300, CurrentHP : " + maxHP);

        if (maxHP == 200)
            GetComponent<Animator>().SetBool("FlameAttack", true);

        if (maxHP == 100)
            GetComponent<Animator>().SetBool("FlyFlameAttack", true);

        if (maxHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
