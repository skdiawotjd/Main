public class Equipment
{
    private string _equipmentnumber;
    private int _equipmentType;
    private string _owernumber;
    private int _stat = -1;
    // 1 - 무기
    private int _statorder = -1;

    public string EquipmentNumber
    {
        get
        {
            return _equipmentnumber;
        }
    }
    public int EquipmentType
    {
        get
        {
            return _equipmentType;
        }
    }
    public string OwerNumber
    {
        get
        {
            return _owernumber;
        }
        set
        {
            _owernumber = value;
        }
    }
    public int Stat
    {
        get
        {
            return _stat;
        }
    }
    public int StatOrder
    {
        get
        {
            return _statorder;
        }
    }

    public Equipment(string NewEquipmentNumber, int NewEquipmentType, string NewOwerNumber, int StatOrder,  int NewStat)
    {
        _equipmentnumber = NewEquipmentNumber;
        _equipmentType = NewEquipmentType;
        _owernumber = NewOwerNumber;
        _statorder = StatOrder;
        _stat = NewStat;
    }
}

/*
 * _equipmentNumber
 * 1000번대 - 무기
 * 
 */
