public class Equipment
{
    private int _equipmentnumber = -1;
    private int _owernumber = -1;
    private int _stat = -1;
    // 1 - 무기
    private int _statorder = -1;

    public int EquipmentNumber
    {
        get
        {
            return _equipmentnumber;
        }
    }
    public int OwerNumber
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

    public int SetStatOrder()
    {
        if (EquipmentNumber == 1)
        {
            return 1;
        }

        return 0;
    }

    public Equipment(int NewEquipmentNumber, int NewOwerNumber, int NewStat)
    {
        _equipmentnumber = NewEquipmentNumber;
        _owernumber = NewOwerNumber;
        _stat = NewStat * 1000;
        _statorder = 1;
    }
}

/*
 * _equipmentNumber
 * 1000번대 - 무기
 * 
 */
