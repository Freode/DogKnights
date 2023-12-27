using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeFloor : MonoBehaviour
{

    [Tooltip("���� ��ġ�� �̵��� ��")]
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���ٴڿ� �������� ��� ���� ��ġ�� �̵�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = target.transform.position;
            collision.gameObject.transform.rotation = target.transform.rotation;
        }
    }
}
