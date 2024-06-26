using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private bool isMelee;

    public int Damage
    {
        get { return damage; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            Vector3 reactVec = other.gameObject.transform.position - transform.position;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            //other.gameObject.GetComponentInParent<Rigidbody>().AddForce(reactVec * 5, ForceMode.Impulse);
            other.gameObject.GetComponentInParent<Player>().knockBack(transform.position);
            Debug.LogWarning("Damaged");
        }
    }

}
