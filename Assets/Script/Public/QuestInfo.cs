using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo
{
    private int _questCode;
    private int _questNumber;
    private int _questClear;
    /// <summary>
    /// ����Ʈ 0�� : ���� ���൵
    /// ����Ʈ 1�� : ��ǥ ���൵
    /// ����Ʈ 2�� : Ȯ���ؾ��� ����
    /// </summary>
    private List<string> _questProcessivity; 
    private int _questReward; // ������ ���� ����

    public QuestInfo(int questCode, int questNumber, int questClear, List<string> questProcessivity, int questReward)
    {
        _questCode = questCode;
        _questNumber = questNumber;
        _questClear = questClear;
        _questProcessivity = questProcessivity;
        _questReward = questReward;


    }
}


