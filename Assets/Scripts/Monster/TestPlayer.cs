using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    public int health = 100;

    Rigidbody rb;
    GameObject nearObject;

    private bool isJumping = false;
    private bool isDamage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0, 0);

        rb.AddForce(movement * speed);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }
    }

    // This function will be called when the player touches the ground
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MonsterAttack")
        {
            if (!isDamage)
            {
                MonsterAttack attack = other.GetComponent<MonsterAttack>();
                health -= attack.Damage;

                //Rock�� if�� �ȿ� ��
                if (other.GetComponent<Rigidbody>() != null)
                    Destroy(other.gameObject); //�÷��̾�� ������ Rock�� Destroy

                Debug.Log("�÷��̾� ���� ü��: " + health);
                StartCoroutine(OnDamage());
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;

        //foreach ����ؼ� ��� ������ ���󺯰�(�ǰݴ����� �� �����)

        yield return new WaitForSeconds(1f);

        isDamage = false;

        //foreach ����ؼ� ��� ������ ���󺯰�(������� ���)
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sword")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sword")
            nearObject = null;
    }
}
