using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    static MonsterManager instance = null;

    [SerializeField]
    private GameObject totem;
    [SerializeField]
    private GameObject slime;
    [SerializeField]
    private GameObject turtleShell;
    [SerializeField]
    private GameObject golem;
    [SerializeField]
    private GameObject middleBoss;
    [SerializeField]
    private GameObject boss;

    public Transform[] pointsFloor0;
    public Transform[] pointsFloor1;
    public Transform[] pointsFloor3;
    public Transform[] pointsFloor5;
    public Transform[] pointsFloor6;
    public Transform[] pointsFloor8;
    public Transform[] pointsFloor10;

    Dictionary<int, Transform[]> floorSpawnPoints = new Dictionary<int, Transform[]>();

    [SerializeField]
    private float spwanTime = 30.0f;

    private int totalMonsterCount;

    private int currentFloor;
    private bool isChangeFloor = false;
    private bool isGameOver = false;

    public FloorManager floorManager;
    public Player player;

    public int TotalMonsterCount
    {
        get { return totalMonsterCount; }
    }
    public static MonsterManager Instance
    {
        get { return instance; }
    }
    public void MonsterKilled()
    {
        totalMonsterCount--;
        Debug.Log("Monster Killed! Remaining: " + totalMonsterCount);
        if (totalMonsterCount == 0)
        {
            StopAllCoroutines();
            FloorManager.Instance.NextStageDoorOpened();
        }
    }

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogError("Another instance of MonsterManager exists!");
            Destroy(gameObject);
            return;
        }
        instance = this;
        totalMonsterCount = 0;
    }

    private void Start()
    {
        floorSpawnPoints[0] = pointsFloor0;
        floorSpawnPoints[1] = pointsFloor1;
        floorSpawnPoints[3] = pointsFloor3;
        floorSpawnPoints[5] = pointsFloor5;
        floorSpawnPoints[6] = pointsFloor6;
        floorSpawnPoints[8] = pointsFloor8;
        floorSpawnPoints[10] = pointsFloor10;

        currentFloor = 0;
        MonsterSpawner(currentFloor);
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
        GameObject[] monsterPrefabs = null;

        if (floorSpawnPoints.TryGetValue(floor, out Transform[] spawnPoints))
        {
            Debug.Log("������ : " + floor);
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
                    monsterPrefabs = new GameObject[] { golem, slime };
                    break;
                case 5:
                    totalMonsterCount = 1;
                    monsterPrefabs = new GameObject[] { middleBoss };
                    break;
                case 6:
                    totalMonsterCount = 3;
                    monsterPrefabs = new GameObject[] { turtleShell };
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
        while (!isGameOver && this.totalMonsterCount > 0)
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

            if (monsterCount < this.totalMonsterCount)
            {
                yield return new WaitForSeconds(spwanTime);

                int idx = Random.Range(0, points.Length);
                int monsterIndex = Random.Range(0, monsterPrefab.Length);
                GameObject monster = monsterPrefab[monsterIndex];
                Instantiate(monster, points[idx].position, monster.transform.rotation);
                Debug.Log("monsterName " + monster.name);
            }
            else
            {
                yield return null;
            }
        }
    }
}
