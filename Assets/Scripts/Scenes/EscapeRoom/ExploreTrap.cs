using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreTrap : MonoBehaviour
{
    // ===== public =====

    [Tooltip("���� ����Ʈ")]
    public GameObject explosion;

    [Tooltip("���� �ּ� �ֱ�")]
    public float minTime = 3.0f;

    [Tooltip("���� �ִ� �ֱ�")]
    public float maxTime = 6.0f;

    // ===== private =====

    // particle system
    private ParticleSystem particle;

    // ���� �ֱ�
    private float explosionCycle = 0.0f;

    // ���� �ð�
    private float playTime = 0.0f;

    // ���� �������� Ȱ��ȭ
    private bool bIsStageActivate = false;

    // ���� ���� ��
    private bool bIsExploring = false;

    // ���� �浹�ϰ� �ִ� ĳ���� ���� ����
    private bool bIsOverlapped = false;

    // Start is called before the first frame update
    void Start()
    {
        particle = explosion.GetComponent<ParticleSystem>();
        explosionCycle = Random.Range(minTime, maxTime + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(bIsStageActivate)
        {
            playTime += Time.deltaTime;

            // Restart explosion trap
            if(playTime >= explosionCycle)
            {
                playTime = 0.0f;
                bIsExploring = true;
                particle.Play();
            }

            if(bIsExploring)
            {
                // validation time of player's hit
                if(playTime >= 1.25)
                {
                    bIsExploring = false;
                }

                // player hit explosion in time
                else if(bIsOverlapped)
                {
                    bIsExploring = false;
                    Debug.Log("Hit");
                    // Add hit method about hp reduction. 
                }
            }
        }
    }

    // Collision Overlapped
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            bIsOverlapped = true;
        }
    }

    // Collision End Overlapped
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            bIsOverlapped = false;
        }
    }

    // Set particle system activate
    public void SetActivate(bool bIsState)
    {
        bIsStageActivate = bIsState;
    }
}
