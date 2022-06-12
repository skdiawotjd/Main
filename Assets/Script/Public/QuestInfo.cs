using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo
{
    private int _questCode;
    private int _questNumber;
    private int _questClear;
    /// <summary>
    /// 리스트 0번 : 현재 진행도
    /// 리스트 1번 : 목표 진행도
    /// 리스트 2번 : 확인해야할 변수
    /// </summary>
    private List<string> _questProcessivity; 
    private int _questReward; // 보상을 위한 변수

    public QuestInfo(int questCode, int questNumber, int questClear, List<string> questProcessivity, int questReward)
    {
        _questCode = questCode;
        _questNumber = questNumber;
        _questClear = questClear;
        _questProcessivity = questProcessivity;
        _questReward = questReward;


    }
}


