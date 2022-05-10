using System.Collections.Generic;

// 리스커 클래스
public class Risker
{
    private int _riskerNumber = -1;
    private int _riskerLevel = -1;
    private double _riskerExp = -1;
    //private List<Equipment> _activeEquipment = new List<Equipment>();

    private int _statStr = -1;
    private int _statInt = -1;
    private int _statDef = -1;
    private int _statAgi = -1;

    private int _physicalAttack = -1;
    private int _magcialAttack = -1;
    private int _hp = -1;
    private int _defense = -1;
    private int _speed = -1;

    //private string ActiveEquipmentNumber = "";
    private int[] _activeEquipmentNumber = new int[4];

    // get/set 모두 가능
    public int RiskerNumber
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
    /*public List<Equipment> ActiveEquipment
    {
        get
        {
            return _activeEquipment;
        }
        set
        {
            _activeEquipment = value;
        }
    }*/
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

    public int[] ActiveEquipmentNumber
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


    public void SetEquipment(int EquipmentNumber)
    {
        if (EquipmentNumber < 1000)
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
        }


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

    public Risker(int NewRiskerNumber, int NewRiskerLevel, double NewRiskerExp, int NewStatStr, int NewStatInt, int NewStatDef, int NewStatAgi)
    {
        RiskerNumber = NewRiskerNumber;
        RiskerLevel = NewRiskerLevel;
        RiskerExp = NewRiskerExp;

        StatStr = NewStatStr;
        StatInt = NewStatInt;
        StatDef = NewStatDef;
        StatAgi = NewStatAgi;


        _physicalAttack = _statStr;
        _magcialAttack = StatInt;
        _hp = StatDef;
        _defense = StatDef;
        _speed = StatAgi;

        for (int i = 0; i < ActiveEquipmentNumber.Length; i++)
        {
            ActiveEquipmentNumber[i] = -1;
        }
    }
}