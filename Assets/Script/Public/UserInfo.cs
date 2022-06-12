using System.Collections.Generic;
//
//
using UnityEngine;
//
//
using UnityEngine.UI;

public class UserInfo
{
    private string _userCode;
    private string _userName;
    private int _userLevel;
    private double _userExp;
    private int _gold;
    private int _diamond;
    private Image _userimage;

    /// <summary>
    /// 리스트 0번 : 메인퀘스트
    /// 리스트 1번 : 일일퀘스트
    /// 리스트 2번 : 주간퀘스트
    /// 리스트 3번 : 긴급퀘스트
    /// </summary>
    private List<QuestInfo> QuestList;

    /*    private List<Risker> _riskers = new List<Risker>();
        private List<Equipment> _equipment = new List<Equipment>();*/

    private int _clearMainQuset;

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
            //_userLevel = value;
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
    public int ClearMainQuset
    {
        get
        {
            return _clearMainQuset;
        }
        set
        {
            _clearMainQuset = value;
        }
    }
    /*    public List<Risker> Riskers
        {
            get
            {
                return _riskers;
            }
            set
            {
                _riskers = value;
            }
        }
        public List<Equipment> Equipment
        {
            get
            {
                return _equipment;
            }
            set
            {
                _equipment = value;
            }
        }*/

    public Dictionary<string, Risker> RiskerDictionary;
    public Dictionary<string, Equipment> EquipmentDictionary;
    public Dictionary<string, QuestInfo> QuestDictionary;

    // 리스커 클래스의 정보 부족, 장비 클래스의 정보
    public UserInfo(string NewUserCode, string NewUserName, int NewUserLevel, double NewUserExp, int NewGold, int NewDiamond,
        string NewRisker, string NewEquipment, string NewQuest)
    /*public UserInfo(string NewUserName, int NewUserLevel, double NewUserExp, int NewGold, int NewDiamond,
        string NewRisker, string NewEquipment)*/
    {
        UserCode = NewUserCode;
        UserName = NewUserName;
        UserLevel = NewUserLevel;
        UserExp = NewUserExp;
        Gold = NewGold;
        Diamond = NewDiamond;

        RiskerDictionary = new Dictionary<string, Risker>();
        EquipmentDictionary = new Dictionary<string, Equipment>();
        QuestDictionary = new Dictionary<string, QuestInfo>();

        // 리스커 저장
        CreateObject(NewRisker, 1);
        // 장비 저장
        CreateObject(NewEquipment, 2);
        // 퀘스트 저장
        //CreateObject(NewQuest, 3);

    }

    public void SetEquipmentToRisker(string SetRiskerNumber, string EquipmentNumber)
    {
        // 리스커안에 있는 stat에 장비의 stat를 더해서 attack등을 set한다
        RiskerDictionary[SetRiskerNumber].SetEquipment(EquipmentDictionary[EquipmentNumber].EquipmentType, EquipmentNumber);
    }

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
            tem_list.Add(tem_str);

            index = StringForCount.IndexOf(SeparatorString, index);
        }
    }

    /// <summary>
    /// ObjectType : 1 - 리스커, 2 - 장비, 3 - 퀘스트
    /// </summary> 
    private void CreateObject(string StringForCount, int ObjectType)
    {
        //[{"EquipmentNumber":"1100","UserName":"11000","Stat":"100","StatOrder":"0"},{"EquipmentNumber":"1101","UserName":"0","Stat":"100","StatOrder":"0"},{"EquipmentNumber":"1102","UserName":"0","Stat":"200","StatOrder":"0"},{"EquipmentNumber":"1103","UserName":"0","Stat":"300","StatOrder":"0"}]
        List<string> tem_list = new List<string>();

        switch (ObjectType)
        { 
            case 1:
                // [{"RiskerNumber":"11000","RiskerLevel":"1","RiskerExp":"0","ActiveEquipmentNumber":["0","0","0","0","0","0"]}]
                // 의 "RiskerNumber":"11000","RiskerLevel":"1","RiskerExp":"0" 값 추출
                SplitString(":\"", '\"', StringForCount, ref tem_list);
                //int ActiveEquipmentNumberOrder = 0;
                // 의 "ActiveEquipmentNumber":["0","0","0","0","0","0"] 값 추출
                SplitString("[\"", ']', StringForCount, ref tem_list);

                for (int i = 0; i < (tem_list.Count / 4); i++)
                {
                    //_riskers.Add(new Risker(i, i + 1, (double)(i * 10), i + 1, i + 1, i + 1, i + 1));
                    //(int NewRiskerNumber, int NewRiskerLevel, double NewRiskerExp, int NewStatStr, int NewStatInt, int NewStatDef, int NewStatAgi)
                    //RiskerDictionary.Add(i, new Risker(i, i + 1, (double)(i * 10), i + 1, i + 1, i + 1, i + 1, "asd"));
                    RiskerDictionary.Add(tem_list[i * 3], new Risker(tem_list[i * 3], int.Parse(string.Format("{0}", tem_list[(i * 3) + 1])),
                        double.Parse(string.Format("{0}", tem_list[(i * 3) + 2])), tem_list[((tem_list.Count / 4) * 3) + i]));
                }
                break;
            case 2:
                // [{"EquipmentNumber":"1100","EquipmentType":"0","OwnerNumber":"11000","StatOrder":"0","Stat":"100"}}]
                SplitString(":\"", '\"', StringForCount, ref tem_list);


                for (int i = 0; i < (tem_list.Count / 5); i++)
                {

                    EquipmentDictionary.Add(tem_list[i * 5], new Equipment(tem_list[i * 5], int.Parse(string.Format("{0}", tem_list[(i * 5) + 1])), 
                        tem_list[(i * 5) + 2], int.Parse(string.Format("{0}", tem_list[(i * 5) + 3])), int.Parse(string.Format("{0}", tem_list[(i * 5) + 4]))));
                    

                    //_equipment.Add(new Equipment(i, -1 , i + 1));
                    //int NewEquipmentNumber, int NewOwerNumber, int NewStat)
                    if (i == 0)
                    {
                        //EquipmentDictionary.Add(i, new Equipment(i, i, i + 1));
                    }
                    else
                    {
                        //EquipmentDictionary.Add(i, new Equipment(i, -1, i + 1));
                    }

                    // NewEquipment에 작성된 값 중 NewOwerNumber가 -1이 아니면 소유하는 리스커넘버가 있음
                    // if문으로 -1이 아니면 조건 걸기



                    /*if (i == 0)
                    {
                        SetEquipmentToRisker(EquipmentDictionary[i].OwerNumber, EquipmentDictionary[i].EquipmentNumber);
                    }*/



                }
                break;
            case 3:
                // 퀘스트 객체 생성
                
                break;
        }


    }
    /*public void ActiveEquipStat(int RiskerNumber)
    {
        for(int EquipmentOrder = 0; EquipmentOrder < RiskerDictionary[RiskerNumber].ActiveEquipmentNumber.Length; EquipmentOrder++)
        {
            if(RiskerDictionary[RiskerNumber].ActiveEquipmentNumber[EquipmentOrder] != -1)
            {
                Debug.Log("order " + EquipmentOrder);

                if (EquipmentOrder < 1000)
                {
                    Debug.Log("능력치 " + EquipmentDictionary[EquipmentOrder].Stat);
                    RiskerStatArray[1] += EquipmentDictionary[EquipmentOrder].Stat;
                }
                else if (EquipmentOrder < 2000)
                {
                    Debug.Log("능력치 " + EquipmentDictionary[EquipmentOrder].Stat);
                    RiskerStatArray[2] += EquipmentDictionary[EquipmentOrder].Stat;
                }
                else if (EquipmentOrder < 3000)
                {
                    Debug.Log("능력치 " + EquipmentDictionary[EquipmentOrder].Stat);
                    RiskerStatArray[3] += EquipmentDictionary[EquipmentOrder].Stat;
                    RiskerStatArray[4] += EquipmentDictionary[EquipmentOrder].Stat;
                }
                else if (EquipmentOrder < 4000)
                {
                    Debug.Log("능력치 " + EquipmentDictionary[EquipmentOrder].Stat);
                    RiskerStatArray[5] += EquipmentDictionary[EquipmentOrder].Stat;
                }
            }
        }
    }*/

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