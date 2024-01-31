﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonsManager : FloorController
{
    // ===== public =====
    [Tooltip("버튼 객체들")]
    public FloorButton[] buttons;

    [Tooltip("다음 층으로 이동할 수 있는 포탈들")]
    public PortalToNextStage[] portals;

    [Tooltip("테스트 모드입니다. : True -> 버튼을 누르는게 1개로 고정.")]
    public bool bIsTestMode = false;

    [Tooltip("현재 탈출맵 층 객체")]
    public GameObject map;

    // ===== private =====

    // 현재 상호작용 버튼 개수
    private int counts = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(FloorButton tempButton in buttons)
        {
            tempButton.SetFloorButtonsManager(this);
        }

        // 7F
        if(portals.Length == 1)
        {
            portals[0].PortalActivate();
        }

        if(bIsTestMode)
        {
            ExploreTrap[] traps = map.gameObject.GetComponents<ExploreTrap>();

            foreach(ExploreTrap tempTrap in traps)
            {
                tempTrap.SetActivate(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 버튼과 플레이어의 상호작용이 발생했을 때, 상호작용한 버튼의 총 개수를 증가시킨다.
    /// </summary>
    public void ButtonCollisionCountIncrease()
    {
        counts++;
        // 누른 버튼 전체 개수 UI와 연동 필요.
        Debug.Log(counts);
        // 리스트에 있는 버튼 총 개수와 누른 버튼 개수 일치.
        if(counts == (bIsTestMode ? 1 : buttons.Length))
        {
            // 다음 스테이지로 이동할 포탈 번호 설정.
            int openPortalNumber = Random.Range(0, portals.Length);

            portals[openPortalNumber].PortalActivate();
        }
    }

    public override void OperateObjects()
    {

        ExploreTrap[] traps = map.gameObject.GetComponents<ExploreTrap>();

        for(int i = 0; i < traps.Length; i++)
        {
            traps[i].SetActivate(true);
        }
    }
}
