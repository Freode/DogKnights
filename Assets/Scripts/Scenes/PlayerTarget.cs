using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    // ===== public =====
    [Tooltip("ȭ��ǥ ��ġ ����")]
    public Vector3 arrowPosition = Vector3.zero;

    [Tooltip("ȭ��ǥ ���� ����")]
    public Vector3 arrowDirection = Vector3.forward;

    [Tooltip("ȭ��ǥ ���� ����")]
    public float arrowLength = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        // ȭ��ǥ ����
        Vector3 direction = transform.forward;
        // ȭ��ǥ �������� ���� ���
        Vector3 arrowStart = transform.position + arrowPosition;
        Vector3 arrowEnd = transform.position + arrowPosition + direction.normalized * arrowLength;

        // ȭ��ǥ �� �׸���
        Gizmos.color = Color.red; // ȭ��ǥ ���� ����
        Gizmos.DrawLine(arrowStart, arrowEnd);

        // ȭ��ǥ �Ӹ� �׸���
        Vector3 arrowHead = arrowEnd - direction.normalized * 0.1f;
        // Gizmos.DrawLine(arrowEnd, arrowHead);
        Gizmos.DrawLine(arrowEnd, arrowEnd + Quaternion.Euler(direction.x, direction.y - 30, 0) * -direction * 0.05f);
        Gizmos.DrawLine(arrowEnd, arrowEnd + Quaternion.Euler(direction.x, direction.y + 30, 0) * -direction * 0.05f);
        // * Vector3.back * 0.05f
    }

    
}
