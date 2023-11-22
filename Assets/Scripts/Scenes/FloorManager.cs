using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{

    static FloorManager instance = null;
    // ===== public =====

    [Tooltip("���� ���� ������ ���� �迭�Դϴ�.")]
    public GameObject[] floors;

    [Tooltip("�÷��̾ �ش� ������ �̵��� ��ġ�Դϴ�.")]
    public GameObject[] targetPlayers;

    [Tooltip("���� ���������� ���� ��, �����ؾ��� ��ü�Դϴ�.")]
    [SerializeField]
    private NextStageDoor[] nextDoorOperaters;
     

    public static FloorManager Instance
    {
        get { return instance; }
    }
    // ===== private =====

    // ���� �÷��̾� ��ġ
    private int currentPlayerFloor = 0;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("systemManager error");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(floors[0].transform.childCount);
        // floors[2].transform.GetChild(1).gameObject.SetActive(false);
        
        // 1F ~ 10F���� ��Ȱ��ȭ
        for(int i = 1; i < floors.Length; i++)
        {
            // ������ �ڽ� ��ü�� ��� ��Ȱ��ȭ
            for(int j = 0; j < floors[i].transform.childCount; j++)
            {
                floors[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ���� ���������� �̵��� �� ȣ���մϴ�. ���� ���������� ���� Invisible�� �����մϴ�.
    /// </summary>
    public void NextStage(GameObject player, int currentStageNum, int nextStageNum)
    {
        // ���� �������� ���������� ���� (Ȱ��ȭ)
        for(int i = 0; i < floors[nextStageNum].transform.childCount; i++)
        {
            floors[nextStageNum].transform.GetChild(i).gameObject.SetActive(true);
        }

        // ���� �������� �������� ���� (��Ȱ��ȭ)
        for(int i = 0; i < floors[currentStageNum].transform.childCount; i++)
        {
            floors[currentStageNum].transform.GetChild(i).gameObject.SetActive(false);
        }

        // �÷��̾� ���� ��ġ�� �̵�
        SetCurrentPlayerFloor(nextStageNum);
        player.transform.position = targetPlayers[nextStageNum].transform.position;
        player.transform.rotation = targetPlayers[nextStageNum].transform.rotation;
    }

    // ���� �÷��̾��� �� ��ġ�� �����մϴ�.
    void SetCurrentPlayerFloor(int _floor)
    {
        currentPlayerFloor = _floor;
    }

    // ���� �÷��̾��� �� ��ġ�� �����ɴϴ�.
    public int GetCurrentPlayerFloor()
    {
        return currentPlayerFloor;
    }

    // ���� ���������� �̵��� �� �ִ� �غ� ������ �����մϴ�.
    public void NextStageDoorOpened()
    {
        nextDoorOperaters[currentPlayerFloor].PrepareNextStageDoorOpened();
    }
}
