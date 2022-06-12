using System.Collections.Generic;

// 리스커 클래스
public class Risker
{
    private string _riskerNumber;
    private int _riskerLevel = -1;
    private double _riskerExp = -1;
    //private List<Equipment> _activeEquipment = new List<Equipment>();

    private int _statStr = 1;
    private int _statInt = 1;
    private int _statDef = 1;
    private int _statAgi = 1;

    private int _physicalAttack = -1;
    private int _magcialAttack = -1;
    private int _hp = -1;
    private int _defense = -1;
    private int _speed = -1;

    //private string ActiveEquipmentNumber = "";
    private string[] _activeEquipmentNumber = new string[2];

    // get/set 모두 가능
    public string RiskerNumber
    {
        get
        {
            return _riskerNumber;
        }
        set
        {
            _riskerNumber = value;
        }
    }
    public int RiskerLevel
    {
        get
        {
            return _riskerLevel;
        }
        set
        {
            _riskerLevel = value;
        }
    }
    public double RiskerExp
    {
        get
        {
            return _riskerExp;
        }
        set
        {
            _riskerExp = value;
        }
    }
    public string[] ActiveEquipment
    {
        get
        {
            return _activeEquipmentNumber;
        }
        set
        {
            _activeEquipmentNumber = value;
        }
    }
    public int StatStr
    {
        get
        {
            return _statStr;
        }
        set
        {
            _statStr = value;
        }
    }
    public int StatInt
    {
        get
        {
            return _statInt;
        }
        set
        {
            _statInt = value;
        }
    }
    public int StatDef
    {
        get
        {
            return _statDef;
        }
        set
        {
            _statDef = value;
        }
    }
    public int StatAgi
    {
        get
        {
            return _statAgi;
        }
        set
        {
            _statAgi = value;
        }
    }

    public int PhysicalAttack
    {
        get
        {
            //_physicalAttack = _statStr + AddEquipmentStat(1);
            return _physicalAttack;
        }
        set
        {
            _physicalAttack = value;
        }
    }
    public int MagcialAttack
    {
        get
        {
            //_magcialAttack = _statInt + AddEquipmentStat(2);
            return _magcialAttack;
        }
        set
        {
            _magcialAttack = value;
        }
    }
    public int Hp
    {
        get
        {
            //_hp = _statDef * 2 + AddEquipmentStat(3);
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    public int Defense
    {
        get
        {
            //_defense = _statDef + +AddEquipmentStat(3);
            return _defense;
        }
        set
        {
            _defense = value;
        }
    }
    public int Speed
    {
        get
        {
            //_speed = _statAgi + +AddEquipmentStat(4);
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    public string[] ActiveEquipmentNumber
    {
        get
        {
            return _activeEquipmentNumber;
        }
        set
        {
            _activeEquipmentNumber = value;
        }
    }

    /*    private int AddEquipmentStat(int StatOrder)
        {

            for (int i = 0; i < ActiveEquipment.Count; i++)
            {
                if (StatOrder == ActiveEquipment[i].StatOrder)
                {
                    return ActiveEquipment[i].Stat;
                }
            }
            return 0;
        }*/


    public void SetEquipment(int EquipmentType, string EquipmentNumber)
    {
        ActiveEquipmentNumber[EquipmentType-1] = EquipmentNumber;

        /*if (EquipmentNumber < 1000)
        {
            ActiveEquipmentNumber[0] = EquipmentNumber;
        }
        else if (EquipmentNumber < 2000)
        {
            ActiveEquipmentNumber[1] = EquipmentNumber;
        }
        else if (EquipmentNumber < 3000)
        {
            ActiveEquipmentNumber[2] = EquipmentNumber;
        }
        else if (EquipmentNumber < 4000)
        {
            ActiveEquipmentNumber[3] = EquipmentNumber;
        }*/


        /*switch(StatOrder)
        {
            // StatOrder이 0이면 무기가 없음
            case 0:
                _physicalAttack = StatStr;
                _magcialAttack = StatInt;
                _hp = StatDef;
                _defense = StatDef;
                _speed = StatAgi;
                break;
            case 1:
                _physicalAttack = StatStr + addStat;
                break;
            case 2:
                _magcialAttack = StatInt + addStat;
                break;
            case 3:
                _hp = StatDef + addStat;
                _defense = StatDef + addStat;
                break;
            case 4:
                _speed = StatAgi + addStat;
                break;
        }*/
    }

    public Risker(string NewRiskerNumber, int NewRiskerLevel, double NewRiskerExp, string NewActiveEquipmentNumber)
    {
        RiskerNumber = NewRiskerNumber;
        RiskerLevel = NewRiskerLevel;
        RiskerExp = NewRiskerExp;

        _physicalAttack = StatStr;
        _magcialAttack = StatInt;
        _hp = StatDef;
        _defense = StatDef;
        _speed = StatAgi;

        int index = -2;
        string tem_str = "";
        List<string> tem_list = new List<string>();

        // 1100","0"
        while (index != -1)
        {
            // 한번 사용 후 이전 값을 지우기 위해 초기화
            tem_str = "";
            //,"에서 2칸 더 가야 값이므로
            index += 2;

            while (NewActiveEquipmentNumber[index] != '\"')
            {
                tem_str += NewActiveEquipmentNumber[index++];
            }
            tem_list.Add(tem_str);

            index = NewActiveEquipmentNumber.IndexOf(",\"", index);
        }

        for (int  i = 0; i < tem_list.Count; i++)
        {
            ActiveEquipment[i] = tem_list[i];
        }
    }
}