using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private GameObject totem;
    [SerializeField] private GameObject slime;
    [SerializeField] private GameObject turtleShell;
    [SerializeField] private GameObject golem;
    [SerializeField] private GameObject boss;

    //���� ���� ��ġ�� ���� �迭
    public Transform[] pointsFloor0;
    public Transform[] pointsFloor1;
    public Transform[] pointsFloor3;
    public Transform[] pointsFloor5;
    public Transform[] pointsFloor6;
    public Transform[] pointsFloor8;
    public Transform[] pointsFloor10;

    // Dictionary�� ����Ͽ� ������ ���� ��ġ�� ����
    Dictionary<int, Transform[]> floorSpawnPoints = new Dictionary<int, Transform[]>();

    //���� ���� �ֱ�
    [SerializeField]
    private float spwanTime = 0.1f;

    private int totalMonsterCount; // �����ִ� �� ���ͼ�

    private int currentFloor; // ���� �� ������
    private bool isChangeFloor = false; // ���� ���� �ٲ�����
    private bool isGameOver = false; // �������� ����

    public FloorManager floorManager;
    public Player player;

    public int TotalMonsterCount
    {
        get { return totalMonsterCount; }
    }

    private void Start()
    {
        currentFloor = 0;
        MonsterSpawner(currentFloor);

        // �� ���� ���� ���� ��ġ �迭�� Dictionary�� �߰�
        floorSpawnPoints[0] = pointsFloor0;
        floorSpawnPoints[1] = pointsFloor1;
        floorSpawnPoints[3] = pointsFloor3;
        floorSpawnPoints[5] = pointsFloor5;
        floorSpawnPoints[6] = pointsFloor6;
        floorSpawnPoints[8] = pointsFloor8;
        floorSpawnPoints[10] = pointsFloor10;
    }
    private void Update()
    {
        isGameOver = player.isDead;
        int newFloor = floorManager.GetCurrentPlayerFloor();
        if (currentFloor != newFloor)
        {
            currentFloor = newFloor;
            isChangeFloor = true;
        }
        if (isChangeFloor)
        {
            MonsterSpawner(currentFloor);
            isChangeFloor = false;
        }
        if (isGameOver)
        {
            StopAllCoroutines();
        }
    }

    void MonsterSpawner(int floor)
    {
        int totalMonsterCount = 0;
        GameObject[] monsterPrefabs = null;

        if (floorSpawnPoints.TryGetValue(floor, out Transform[] spawnPoints))
        {
            switch (floor)
            {
                case 0:
                    totalMonsterCount = 1;
                    monsterPrefabs = new GameObject[] { totem };
                    break;
                case 1:
                    totalMonsterCount = 4;
                    monsterPrefabs = new GameObject[] { slime };
                    break;
                case 3:
                    totalMonsterCount = 9;
                    monsterPrefabs = new GameObject[] { slime, golem, turtleShell };
                    break;
                case 5:
                    totalMonsterCount = 1;
                    monsterPrefabs = new GameObject[] { boss };
                    break;
                case 6:
                    totalMonsterCount = 3;
                    monsterPrefabs = new GameObject[] { golem };
                    break;
                case 8:
                    totalMonsterCount = 8;
                    monsterPrefabs = new GameObject[] { golem, turtleShell };
                    break;
                case 10:
                    totalMonsterCount = 1;
                    monsterPrefabs = new GameObject[] { boss };
                    break;
            }

            if (monsterPrefabs != null && spawnPoints != null)
            {
                StartCoroutine(CreateMonster(monsterPrefabs, totalMonsterCount, spawnPoints));
            }
        }
    }

    IEnumerator CreateMonster(GameObject[] monsterPrefab, int totalMonsterCount, Transform[] points)
    {
        Debug.Log("CreateMonster coroutine started.");
        while (!isGameOver)
        {
            //���� ������ ���� ���� ����
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

            if (monsterCount < totalMonsterCount)
            {
                //������ ���� �ֱ� �ð���ŭ ���
                yield return new WaitForSeconds(spwanTime);

                int idx = Random.Range(1, points.Length);
                int monster = Random.Range(0, monsterPrefab.Length);
                Instantiate(monsterPrefab[monster], points[idx].position, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }
}
