using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    GameObject totem;
    [SerializeField]
    GameObject slime;
    [SerializeField]
    GameObject turtleShell;
    [SerializeField]
    GameObject golem;
    [SerializeField]
    GameObject boss;
   
    [SerializeField]
    private float createTime; //���͸� �߻���ų �ֱ�

    private int totalMonsterCount;
    private int countTotem;
    private int countSlime;
    private int countTurtleShell;
    private int countGolem;
    private int countBoss;

    private int currentFloor; // ���� �� ������
    private bool isGameOver = false; // �������� ����

    public int TotalMonsterCount
    {
        get { return totalMonsterCount; }
    }

    private void Awake()
    {
        currentFloor = 0;
        MonsterSpawner(currentFloor);
    }
    private void Update()
    {
        isGameOver = GameManager.Instance.Player.isDead;
        if (currentFloor != getFloorState())
            MonsterSpawner(currentFloor);
    }

    private int getFloorState()
    {
        return 0;
    }

    void MonsterSpawner(int floor)
    {
        switch (floor)
        {
            case 0:
                totalMonsterCount = countTotem;
                break;
            case 1:
                break;
            case 3:
                break;
            case 5:
                break;
            case 6:
                break;
            case 8:
                break;
            case 10:
                break;
        }
    }

    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            //���� ������ ���� ���� ����
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

            if (monsterCount < totalMonsterCount)
            {
                //������ ���� �ֱ� �ð���ŭ ���
                yield return new WaitForSeconds(createTime);

                //�ұ�Ģ���� ��ġ ����
                //int idx = Random.Range(1, points.Length);
                //������ ���� ����
                //Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }
}
