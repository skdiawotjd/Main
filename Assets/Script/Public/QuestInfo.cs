using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo
{
    private int _questCode;
    private int _questNumber;
    private int _questClear;
    private int _questReward; // 보상을 위한 변수

    /// <summary>
    /// 리스트 0번 : 현재 진행도
    /// 리스트 1번 : 목표 진행도
    /// 리스트 2번 : 확인해야할 변수
    /// </summary>
    private List<string> _questProcessivity; 

    public QuestInfo(int questCode, int questNumber, int questClear, int questReward, string questProcessivity)
    {
        _questCode = questCode;
        _questNumber = questNumber;
        _questClear = questClear;
        _questReward = questReward;
        _questProcessivity = new List<string>();

        int index = -2;
        string tem_str = "";

        // 5","6","7"
        while (index != -1)
        {
            // 한번 사용 후 이전 값을 지우기 위해 초기화
            tem_str = "";
            //,"에서 2칸 더 가야 값이므로
            index += 2;

            while (questProcessivity[index] != '\"')
            {
                tem_str += questProcessivity[index++];
            }
            _questProcessivity.Add(tem_str);

            index = questProcessivity.IndexOf(",\"", index);
        }

    }
}


