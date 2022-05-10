public class Equipment
{
    private int _equipmentNumber = -1;
    private int _owerNumber = -1;
    private int _stat = -1;

    public int EquipmentNumber
    {
        get
        {
            return _equipmentNumber;
        }
    }
    public int OwerNumber
    {
        get
        {
            return _owerNumber;
        }
    }
    public int Stat
    {
        get
        {
            return _stat;
        }
    }

    public int StatOrder()
    {
        if (EquipmentNumber == 1)
        {
            return 1;
        }

        return 0;
    }

    public Equipment(int NewEquipmentNumber, int NewOwerNumber, int NewStat)
    {
        _equipmentNumber = NewEquipmentNumber;
        _owerNumber = NewOwerNumber;
        _stat = NewStat * 1000;
    }
}

/*
 * _equipmentNumber
 * 1000번대 - 무기
 * 
 */
