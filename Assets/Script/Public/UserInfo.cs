using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    private string _userCode;
    private string _userName;
    private int _userLevel;
    private double _userExp;
    private int _gold;
    private int _diamond;

    /*/// <summary>
    /// ����Ʈ 0�� : ��������Ʈ
    /// ����Ʈ 1�� : ��������Ʈ
    /// ����Ʈ 2�� : �ְ�����Ʈ
    /// ����Ʈ 3�� : �������Ʈ
    /// </summary>
    private List<QuestInfo> QuestList;


    private int _clearMainQuset;*/

    int[] RiskerStatArray = new int[6];

    public string UserCode
    {
        get
        {
            return _userCode;
        }
        set
        {
            _userCode = value;
        }
    }
    public string UserName
    {
        get
        {
            return _userName;
        }
        set
        {
            _userName = value;
        }
    }
    public int UserLevel
    {
        get
        {
            return _userLevel;
        }
        set
        {
            _userLevel = value;
        }
    }
    public double UserExp
    {
        get
        {
            return _userExp;
        }
        set
        {
            _userExp = value;
        }
    }
    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
        }
    }
    public int Diamond
    {
        get
        {
            return _diamond;
        }
        set
        {
            _diamond = value;
        }
    }
    /*public int ClearMainQuset
    {
        get
        {
            return _clearMainQuset;
        }
        set
        {
            _clearMainQuset = value;
        }
    }*/

    public Dictionary<string, Risker> RiskerDictionary;
    public Dictionary<string, Equipment> EquipmentDictionary;
    public Dictionary<string, QuestInfo> QuestDictionary;

    public UserInfo(string NewUserCode, string NewUserName, int NewUserLevel, double NewUserExp, int NewGold, int NewDiamond,
        string NewQuest, string NewRisker, string NewEquipment)
    {
        UserCode = NewUserCode;
        UserName = NewUserName;
        UserLevel = NewUserLevel;
        UserExp = NewUserExp;
        Gold = NewGold;
        Diamond = NewDiamond;

        QuestDictionary = new Dictionary<string, QuestInfo>();
        RiskerDictionary = new Dictionary<string, Risker>();
        EquipmentDictionary = new Dictionary<string, Equipment>();

        // ����Ʈ ����
        CreateObject(NewQuest, 1);
        // ����Ŀ ����
        CreateObject(NewRisker, 2);
        // ��� ����
        CreateObject(NewEquipment, 3);

    }

    /// <summary>
    /// SeparatorString - ����, UntilString - ����, StringForCount - ��, tem_list - �� ����
    /// </summary>
    private void SplitString(string SeparatorString, char UntilString, string StringForCount, ref List<string> tem_list)
    {
        int index = 0;
        string tem_str = "";

        Debug.Log(StringForCount);
        for (index = StringForCount.IndexOf(SeparatorString, index); index != -1;)
        {
            // �ѹ� ��� �� ���� ���� ����� ���� �ʱ�ȭ
            tem_str = "";
            //:"���� 2ĭ �� ���� ���̹Ƿ�
            index += 2;
            // "�� ���� �� ���� ��� �ӽ� str�� �߰� (:"0000")
            while (StringForCount[index] != UntilString)
            {
                tem_str += StringForCount[index++];
            }
            //Debug.Log(tem_str);
            tem_list.Add(tem_str);

            index = StringForCount.IndexOf(SeparatorString, index);
        }
    }

    /// <summary>
    /// ObjectType : 1 - ����Ʈ, 2 - ����Ŀ, 3 - ���
    /// </summary> 
    private void CreateObject(string StringForCount, int ObjectType)
    {
        List<string> tem_list = new List<string>();

        switch (ObjectType)
        { 
            case 1:
                // [{"QuestCode":"1","QuestNumber":"2","QuestClear":"3","QuestReward":"4","QuestProcessivity":["5","6","7"]}]����
                // "QuestCode":"1","QuestNumber":"2","QuestClear":"3","QuestReward":"4" �� ����
                SplitString(":\"", '\"', StringForCount, ref tem_list);
                // "QuestProcessivity":["5","6","7"] �� ����
                SplitString("[\"", ']', StringForCount, ref tem_list);

                for (int i = 0; i < (tem_list.Count / 5); i++)
                {
                    QuestDictionary.Add(tem_list[i * 4], new QuestInfo(int.Parse(string.Format("{0}", tem_list[(i * 4)])), int.Parse(string.Format("{0}", tem_list[(i * 4) + 1])),
                        int.Parse(string.Format("{0}", tem_list[(i * 4) + 2])), int.Parse(string.Format("{0}", tem_list[(i * 4) + 3])), tem_list[((tem_list.Count / 5) * 4) + i]));
                }

                break;
            case 2:
                // [{"RiskerNumber":"11000","RiskerLevel":"1","RiskerExp":"0","ActiveEquipmentNumber":["0","0","0","0","0","0"]}]����
                // "RiskerNumber":"11000","RiskerLevel":"1","RiskerExp":"0" �� ����
                SplitString(":\"", '\"', StringForCount, ref tem_list);
                // "ActiveEquipmentNumber":["0","0","0","0","0","0"] �� ����
                SplitString("[\"", ']', StringForCount, ref tem_list);

                for (int i = 0; i < (tem_list.Count / 4); i++)
                {
                    RiskerDictionary.Add(tem_list[i * 3], new Risker(tem_list[i * 3], int.Parse(string.Format("{0}", tem_list[(i * 3) + 1])),
                        double.Parse(string.Format("{0}", tem_list[(i * 3) + 2])), tem_list[((tem_list.Count / 4) * 3) + i]));
                }
                break;
            case 3:
                // [{"EquipmentNumber":"1100","EquipmentType":"0","OwnerNumber":"11000","StatOrder":"0","Stat":"100"}}]����
                // "EquipmentNumber":"1100","EquipmentType":"0","OwnerNumber":"11000","StatOrder":"0","Stat":"100" �� ����
                SplitString(":\"", '\"', StringForCount, ref tem_list);

                for (int i = 0; i < (tem_list.Count / 5); i++)
                {

                    EquipmentDictionary.Add(tem_list[i * 5], new Equipment(tem_list[i * 5], int.Parse(string.Format("{0}", tem_list[(i * 5) + 1])),
                        tem_list[(i * 5) + 2], int.Parse(string.Format("{0}", tem_list[(i * 5) + 3])), int.Parse(string.Format("{0}", tem_list[(i * 5) + 4]))));
                }

                break;
            default:
                Debug.Log("CreateObject�Լ��� ObjectType�� �߸���");
                break;
        }



    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SetEquipmentToRisker(string SetRiskerNumber, string EquipmentNumber)
    {
        // ����Ŀ�ȿ� �ִ� stat�� ����� stat�� ���ؼ� attack���� set�Ѵ�
        RiskerDictionary[SetRiskerNumber].SetEquipment(EquipmentDictionary[EquipmentNumber].EquipmentType, EquipmentNumber);
    }

    /// <summary>
    /// ����Ŀ ���ݷ�/hp/ü��/���ǵ� ���
    /// </summary>
    public int[] RiskerStat(string RiskerNumber)
    {
        RiskerStatArray[0] = RiskerDictionary[RiskerNumber].RiskerLevel;
        RiskerStatArray[1] = RiskerDictionary[RiskerNumber].PhysicalAttack;
        RiskerStatArray[2] = RiskerDictionary[RiskerNumber].MagcialAttack;
        RiskerStatArray[3] = RiskerDictionary[RiskerNumber].Hp;
        RiskerStatArray[4] = RiskerDictionary[RiskerNumber].Defense;
        RiskerStatArray[5] = RiskerDictionary[RiskerNumber].Speed;


        return RiskerStatArray;
    }


}