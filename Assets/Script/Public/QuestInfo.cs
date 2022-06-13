using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo
{
    private int _questCode;
    private int _questNumber;
    private int _questClear;
    private int _questReward; // ������ ���� ����

    /// <summary>
    /// ����Ʈ 0�� : ���� ���൵
    /// ����Ʈ 1�� : ��ǥ ���൵
    /// ����Ʈ 2�� : Ȯ���ؾ��� ����
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
            // �ѹ� ��� �� ���� ���� ����� ���� �ʱ�ȭ
            tem_str = "";
            //,"���� 2ĭ �� ���� ���̹Ƿ�
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


