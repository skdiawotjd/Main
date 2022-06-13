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
    /// 리스트 0번 : 메인퀘스트
    /// 리스트 1번 : 일일퀘스트
    /// 리스트 2번 : 주간퀘스트
    /// 리스트 3번 : 긴급퀘스트
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

        // 퀘스트 저장
        CreateObject(NewQuest, 1);
        // 리스커 저장
        CreateObject(NewRisker, 2);
        // 장비 저장
        CreateObject(NewEquipment, 3);

    }

    /// <summary>
    /// SeparatorString - 에서, UntilString - 까지, StringForCount - 를, tem_list - 에 저장
    /// </summary>
    private void SplitString(string SeparatorString, char UntilString, string StringForCount, ref List<string> tem_list)
    {
        int index = 0;
        string tem_str = "";

        Debug.Log(StringForCount);
        for (index = StringForCount.IndexOf(SeparatorString, index); index != -1;)
        {
            // 한번 사용 후 이전 값을 지우기 위해 초기화
            tem_str = "";
            //:"에서 2칸 더 가야 값이므로
            index += 2;
            // "가 나올 때 까지 계속 임시 str에 추가 (:"0000")
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
    /// ObjectType : 1 - 퀘스트, 2 - 리스커, 3 - 장비
    /// </summary> 
    private void CreateObject(string StringForCount, int ObjectType)
    {
        List<string> tem_list = new List<string>();

        switch (ObjectType)
        { 
            case 1:
                // [{"QuestCode":"1","QuestNumber":"2","QuestClear":"3","QuestReward":"4","QuestProcessivity":["5","6","7"]}]에서
                // "QuestCode":"1","QuestNumber":"2","QuestClear":"3","QuestReward":"4" 값 추출
                SplitString(":\"", '\"', StringForCount, ref tem_list);
                // "QuestProcessivity":["5","6","7"] 값 추출
                SplitString("[\"", ']', StringForCount, ref tem_list);

                for (int i = 0; i < (tem_list.Count / 5); i++)
                {
                    QuestDictionary.Add(tem_list[i * 4], new QuestInfo(int.Parse(string.Format("{0}", tem_list[(i * 4)])), int.Parse(string.Format("{0}", tem_list[(i * 4) + 1])),
                        int.Parse(string.Format("{0}", tem_list[(i * 4) + 2])), int.Parse(string.Format("{0}", tem_list[(i * 4) + 3])), tem_list[((tem_list.Count / 5) * 4) + i]));
                }

                break;
            case 2:
                // [{"RiskerNumber":"11000","RiskerLevel":"1","RiskerExp":"0","ActiveEquipmentNumber":["0","0","0","0","0","0"]}]에서
                // "RiskerNumber":"11000","RiskerLevel":"1","RiskerExp":"0" 값 추출
                SplitString(":\"", '\"', StringForCount, ref tem_list);
                // "ActiveEquipmentNumber":["0","0","0","0","0","0"] 값 추출
                SplitString("[\"", ']', StringForCount, ref tem_list);

                for (int i = 0; i < (tem_list.Count / 4); i++)
                {
                    RiskerDictionary.Add(tem_list[i * 3], new Risker(tem_list[i * 3], int.Parse(string.Format("{0}", tem_list[(i * 3) + 1])),
                        double.Parse(string.Format("{0}", tem_list[(i * 3) + 2])), tem_list[((tem_list.Count / 4) * 3) + i]));
                }
                break;
            case 3:
                // [{"EquipmentNumber":"1100","EquipmentType":"0","OwnerNumber":"11000","StatOrder":"0","Stat":"100"}}]에서
                // "EquipmentNumber":"1100","EquipmentType":"0","OwnerNumber":"11000","StatOrder":"0","Stat":"100" 값 추출
                SplitString(":\"", '\"', StringForCount, ref tem_list);

                for (int i = 0; i < (tem_list.Count / 5); i++)
                {

                    EquipmentDictionary.Add(tem_list[i * 5], new Equipment(tem_list[i * 5], int.Parse(string.Format("{0}", tem_list[(i * 5) + 1])),
                        tem_list[(i * 5) + 2], int.Parse(string.Format("{0}", tem_list[(i * 5) + 3])), int.Parse(string.Format("{0}", tem_list[(i * 5) + 4]))));
                }

                break;
            default:
                Debug.Log("CreateObject함수의 ObjectType이 잘못됨");
                break;
        }



    }

    /// <summary>
    /// 무기 장착
    /// </summary>
    public void SetEquipmentToRisker(string SetRiskerNumber, string EquipmentNumber)
    {
        // 리스커안에 있는 stat에 장비의 stat를 더해서 attack등을 set한다
        RiskerDictionary[SetRiskerNumber].SetEquipment(EquipmentDictionary[EquipmentNumber].EquipmentType, EquipmentNumber);
    }

    /// <summary>
    /// 리스커 공격력/hp/체력/스피드 출력
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