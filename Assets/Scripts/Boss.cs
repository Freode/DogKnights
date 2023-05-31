using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Base
{
    [SerializeField]
    private Transform player; // �÷��̾��� ��ġ
    private bool isFlipped = false; // ������ ���� ������ �ִ���

    [SerializeField]
    private int attackDamage = 20;
    [SerializeField]
    private int FlameAttackDemage = 40;
    [SerializeField]
    private Vector3 attackOffset;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private LayerMask attackMask;

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
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if(colInfo != null)
        {
            //colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDemage);
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
            //colInfo.GetComponent<PlayerHealth>().TakeDamage(FlameAttackDemage);
        }
    }
}
