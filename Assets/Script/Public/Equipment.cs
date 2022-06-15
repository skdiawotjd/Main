public class Equipment
{
    private string _equipmentnumber;
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

    public Equipment(string NewEquipmentNumber, string NewOwerNumber, int StatOrder,  int NewStat)
    {
        _equipmentnumber = NewEquipmentNumber;
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
