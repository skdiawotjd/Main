using System.Collections.Generic;
//
//
using UnityEngine;
//
//
using UnityEngine.UI;

public class UserInfo
{
    private string _userName;
    private int _userLevel;
    private double _userExp;
    private int _gold;
    private int _diamond;
    private Image _userimage;

    /*    private List<Risker> _riskers = new List<Risker>();
        private List<Equipment> _equipment = new List<Equipment>();*/

    private int _clearMainQuset;

    int[] RiskerStatArray = new int[6];

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

    public Dictionary<int, Risker> RiskerDictionary;
    public Dictionary<int, Equipment> EquipmentDictionary;

    // 리스커 클래스의 정보 부족, 장비 클래스의 정보
    public UserInfo(string NewUserName, int NewUserLevel, double NewUserExp, int NewGold, int NewDiamond,
        int NewCountRisker, string NewRisker, int NewCountEquipment, string NewEquipment)
    {
        _userName = NewUserName;
        _userLevel = NewUserLevel;
        _userExp = NewUserExp;
        _gold = NewGold;
        _diamond = NewDiamond;

        RiskerDictionary = new Dictionary<int, Risker>();
        EquipmentDictionary = new Dictionary<int, Equipment>();

        //
        // 임시
        for (int i = 0; i < NewCountRisker; i++)
        {
            //_riskers.Add(new Risker(i, i + 1, (double)(i * 10), i + 1, i + 1, i + 1, i + 1));
            //(int NewRiskerNumber, int NewRiskerLevel, double NewRiskerExp, int NewStatStr, int NewStatInt, int NewStatDef, int NewStatAgi)
            RiskerDictionary.Add(i, new Risker(i, i + 1, (double)(i * 10), i + 1, i + 1, i + 1, i + 1));
        }

        for (int i = 0; i < NewCountEquipment; i++)
        {
            //_equipment.Add(new Equipment(i, -1 , i + 1));
            //int NewEquipmentNumber, int NewOwerNumber, int NewStat)
            if (i == 0)
            {
                EquipmentDictionary.Add(i, new Equipment(i, i, i + 1));
            }
            else
            {
                EquipmentDictionary.Add(i, new Equipment(i, -1, i + 1));
            }

            // NewEquipment에 작성된 값 중 NewOwerNumber가 -1이 아니면 소유하는 리스커넘버가 있음
            // if문으로 -1이 아니면 조건 걸기
            if (i == 0)
            {
                SetEquipmentToRisker(EquipmentDictionary[i].OwerNumber, EquipmentDictionary[i].EquipmentNumber);
            }
        }
        //
        //
    }

    public void SetEquipmentToRisker(int SetRiskerNumber, int EquipmentNumber)
    {
        // 리스커안에 있는 stat에 장비의 stat를 더해서 attack등을 set한다
        RiskerDictionary[SetRiskerNumber].SetEquipment(EquipmentNumber);
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

    public int[] RiskerStat(int RiskerNumber)
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