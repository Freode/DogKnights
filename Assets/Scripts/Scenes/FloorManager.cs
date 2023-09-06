using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    // ===== public =====

    [Tooltip("층에 대한 정보를 모은 배열입니다.")]
    public GameObject[] floors;

    [Tooltip("플레이어가 해당 층으로 이동할 위치입니다.")]
    public GameObject[] targetPlayers;

    // ===== private =====

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(floors[0].transform.childCount);
        // floors[2].transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 다음 스테이지로 이동할 때 호출합니다. 현재 스테이지의 맵을 Invisible로 설정합니다.
    /// </summary>
    public void NextStage(GameObject player, int currentStageNum, int nextStageNum)
    {
        // 다음 스테이지 불투명으로 변경 (활성화)
        for(int i = 0; i < floors[nextStageNum].transform.childCount; i++)
        {
            floors[nextStageNum].transform.GetChild(i).gameObject.SetActive(true);
        }

        // 이전 스테이지 투명으로 변경 (비활성화)
        for(int i = 0; i < floors[currentStageNum].transform.childCount; i++)
        {
            floors[currentStageNum].transform.GetChild(i).gameObject.SetActive(false);
        }

        // 플레이어 다음 위치로 이동
        player.transform.position = targetPlayers[nextStageNum].transform.position;
        player.transform.rotation = targetPlayers[nextStageNum].transform.rotation;
    }

}
