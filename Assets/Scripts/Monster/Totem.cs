using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth;
    [SerializeField]
    protected float curHealth;

    BoxCollider boxCollider;
    MeshRenderer mat;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            Sword sword = other.GetComponent<Sword>();
            curHealth -= sword.damage;

            Debug.Log("Sword : " + curHealth);
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        mat.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.material.color = Color.white;
        }
        else //���
        {
            mat.material.color = Color.gray;  
            Destroy(gameObject, 2);

        }
    }
}
