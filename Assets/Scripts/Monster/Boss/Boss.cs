using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int BossHP = 300;
    [SerializeField]
    private int attackDamage = 1;
    [SerializeField]
    private int FlameAttackDamage = 2;
    [SerializeField]
    private int FlyFlameAttackDamge = 2;

    [SerializeField]
    private Vector3 attackOffset; // ĳ���͸� �߽����� ������ �Ÿ�
    [SerializeField]
    private float attackRange = 5f; // ���� �����Ÿ�
    [SerializeField]
    private LayerMask attackMask;

    private GameObject deathEffect;
    public bool isInvulnerable = false; // ����

    public int _BossHP
    {
        get { return BossHP; }
    }

    public void Attack()
    {
        Debug.Log("Attack");
        Vector3 pos = transform.position;
        // ���� ����
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        BossHP -= damage;
        Debug.Log("MaxHP : 300, CurrentHP : " + BossHP);
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
