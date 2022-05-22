using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoManager : MonoBehaviour
{
    private UserInfoManager UserInfoManager;

    // ����Ŀ ����Ʈ�� ������ �θ� ������Ʈ
    public GameObject RiskerContent;
    // ��� ����Ʈ�� ������ �θ� ������Ʈ
    public GameObject EquipmentContent;

    // ���¿� ���� �����ִ� ���� ��ȭ��ų ������Ʈ
    public GameObject CharacterStat;
    public GameObject CharacterScrollView;
    public GameObject EquipmentScrollView;

    // ������ ������ ���� �̹��� ��� ������Ʈ
    public GameObject Variation;
    private Image[] VariationGroup;
    private int[] PreStat;

    // ����Ŀ ����
    public GameObject TextVal;
    private TextMeshProUGUI[] TextValGroup;

    // ���õ� ����Ŀ / ���
    private int SelectedRiskerNumber = -1;
    private int SelectedEquipmentNumber = -1;

    void Start()
    {
        UserInfoManager = GameObject.Find("UserInfoManager").GetComponent<UserInfoManager>();
        // ����Ŀ ����Ʈ ����
        CreateRiskerList();
        // ��� ����Ʈ ����
        CreateEquipmentList();
        // ����Ŀ ���� ���� �ʱ�ȭ
        TextValGroup = new TextMeshProUGUI[6];
        for (int i = 0; i < TextValGroup.Length; i++)
        {
            TextValGroup[i] = TextVal.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
        // ���� ���� Ȯ�ο� ���� �ʱ�ȭ
        VariationGroup = new Image[5];
        for (int i = 0; i < VariationGroup.Length; i++)
        {
            VariationGroup[i] = Variation.transform.GetChild(i + 1).GetComponent<Image>();
        }
        PreStat = new int[5];
        for (int i = 0; i < PreStat.Length; i++)
        {
            PreStat[i] = -1;
        }
    }


    private void CreateRiskerList()
    {
        foreach (var Risker in UserInfoManager.UserInfo.RiskerDictionary)
        {
            int ListRiskerNumber = Risker.Value.RiskerNumber;
            GameObject btn = Instantiate(Resources.Load("RiskerButton"), RiskerContent.transform) as GameObject;
            btn.GetComponent<Button>().onClick.AddListener(() => { ClickRisker(ListRiskerNumber); });
        }
    }

    private void CreateEquipmentList()
    {
        foreach (var Equipment in UserInfoManager.UserInfo.EquipmentDictionary)
        {
            int ListEquipmentNumber = Equipment.Value.EquipmentNumber;
            if (Equipment.Value.OwerNumber == -1)
            {
                GameObject btn = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
                btn.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(ListEquipmentNumber); });
            }
        }
    }

    private void ClickRisker(int RiskerNumber)
    {
        // ���ϰ� �Ⱥ��ϰ� ����
        SetActiveObject(true);
        SelectedRiskerNumber = RiskerNumber;
        // ������ ������� set
        InitializeVariation();

        // ù RiskerStat�� Initialize
        // 1. ����Ŀ�� �⺻ ���� ���
        UpdateRiskerStat(SelectedRiskerNumber);
        // 2. ����Ŀ�� ������ ��� ������ ��� ���� ���ϱ�

        for (int i = 0; i < UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber.Length; i++)
        {
            if (UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i] != -1)
            {
                SelectedEquipmentNumber = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i];
                UpdateEquipmentStat();
            }
        }
        // 3. �������� Stat�� PreStat�� ����
        SetRiskerStat();
    }
    private void ClickEquipment(int EquipmentNumber)
    {
        SelectedEquipmentNumber = EquipmentNumber;
        //UserInfoManager.SetChangeEquipment(SelectedRiskerNumber, EquipmentNumber);
        Debug.Log("�ش� ����� �ѹ� " + EquipmentNumber);
        Debug.Log("�ش� ����� ���� " + UserInfoManager.UserInfo.EquipmentDictionary[EquipmentNumber].Stat);


        UpdateEquipmentStat();
        UpdateVariation(CheckEquipmentNumber());
        //asd asd
    }


    public void SetEquipment()
    {
        GameObject btn = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
        int tem = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[CheckEquipmentNumber()];
        btn.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(tem); });
        // ����Ŀ�� ActiveEquipmentNumber�� ����
        UserInfoManager.UserInfo.SetEquipmentToRisker(SelectedRiskerNumber, SelectedEquipmentNumber);
        // ��� ����Ʈ ����

        // �������� Stat�� PreStat�� ����
        SetRiskerStat();
        // ������ ������� set
        InitializeVariation();

    }

    private void UpdateRiskerStat(int ListRiskerNumber)
    {
        for (int i = 0; i < 6; i++)
        {
            TextValGroup[i].text = UserInfoManager.UserInfo.RiskerStat(ListRiskerNumber)[i].ToString();
        }
    }

    private int CheckEquipmentNumber()
    {
        if (SelectedEquipmentNumber < 1000)
        {
            return 0;
        }
        else if (SelectedEquipmentNumber < 2000)
        {
            return 1;
        }
        else if (SelectedEquipmentNumber < 3000)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    private void UpdateEquipmentStat()
    {
        if (SelectedEquipmentNumber < 1000)
        {
            // ���� ���Ȱ� ���� ������ ����
            Debug.Log("�ɷ�ġ " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
            //
            //PreStat[0] = int.Parse(string.Format("{0}", TextValGroup[1].text));
            //
            TextValGroup[1].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[1] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
            /*// ����� ���Ȱ����� ���� Ȯ��
            UpdateVariation(0);
            // �� ��ư ����
            GameObject btn = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
            int tem = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[0];
            btn.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(tem); });
            // �ٲ� ��� Rikser�� ����
            SetEquipment();*/
        }
        else if (SelectedEquipmentNumber < 2000)
        {
            Debug.Log("�ɷ�ġ " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
            //
            //PreStat[1] = int.Parse(string.Format("{0}", TextValGroup[1].text));
            //
            TextValGroup[2].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[2] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
            /*UpdateVariation(1);
            GameObject btn = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
            btn.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[1]); });*/
        }
        else if (SelectedEquipmentNumber < 3000)
        {
            Debug.Log("�ɷ�ġ " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
            //
            //PreStat[2] = int.Parse(string.Format("{0}", TextValGroup[1].text));
            //
            TextValGroup[3].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[3] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
            /*UpdateVariation(2);*/
            //
            //PreStat[3] = int.Parse(string.Format("{0}", TextValGroup[1].text));
            //
            TextValGroup[4].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[4] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
            /*UpdateVariation(3);
            GameObject btn = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
            btn.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[2]); });*/
        }
        else if (SelectedEquipmentNumber < 4000)
        {
            Debug.Log("�ɷ�ġ " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
            //
            //PreStat[4] = int.Parse(string.Format("{0}", TextValGroup[1].text));
            //
            TextValGroup[5].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[5] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
            /*UpdateVariation(4);
            GameObject btn = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
            btn.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[3]); });*/
        }
    }

    private void UpdateVariation(int StatOrder)
    {
        Debug.Log(TextValGroup[StatOrder + 1].text + " - " + PreStat[StatOrder].ToString());
        if (int.Parse(string.Format("{0}", TextValGroup[StatOrder + 1].text)) - PreStat[StatOrder] > 0)
        {
            Debug.Log("����");
            VariationGroup[StatOrder].color = new Color32(255, 0, 0, 255);
        }
        else if (int.Parse(string.Format("{0}", TextValGroup[StatOrder + 1].text)) - PreStat[StatOrder] == 0)
        {
            Debug.Log("�״��");
            VariationGroup[StatOrder].color = new Color32(255, 255, 255, 255);
        }
        else if (int.Parse(string.Format("{0}", TextValGroup[StatOrder + 1].text)) - PreStat[StatOrder] < 0)
        {
            Debug.Log("����");
            VariationGroup[StatOrder].color = new Color32(0, 0, 255, 255);
        }
    }

    private void InitializeVariation()
    {
        VariationGroup[0].color = new Color32(255, 255, 255, 255);
        VariationGroup[1].color = new Color32(255, 255, 255, 255);
        VariationGroup[2].color = new Color32(255, 255, 255, 255);
        VariationGroup[3].color = new Color32(255, 255, 255, 255);
        VariationGroup[4].color = new Color32(255, 255, 255, 255);
    }

    // 3. �������� Stat�� Risker�� Stat�� ����
    public void SetStat()
    {
        UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].PhysicalAttack = int.Parse(string.Format("{0}", TextValGroup[1].text));
        UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].MagcialAttack = int.Parse(string.Format("{0}", TextValGroup[2].text));
        UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].Hp = int.Parse(string.Format("{0}", TextValGroup[3].text));
        UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].Defense = int.Parse(string.Format("{0}", TextValGroup[4].text));
        UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].Speed = int.Parse(string.Format("{0}", TextValGroup[5].text));
    }

    private void SetRiskerStat()
    {
        PreStat[0] = int.Parse(string.Format("{0}", TextValGroup[1].text));
        PreStat[1] = int.Parse(string.Format("{0}", TextValGroup[2].text));
        PreStat[2] = int.Parse(string.Format("{0}", TextValGroup[3].text));
        PreStat[3] = int.Parse(string.Format("{0}", TextValGroup[4].text));
        PreStat[4] = int.Parse(string.Format("{0}", TextValGroup[5].text));
    }


    public void SetActiveObject(bool Visible)
    {
        CharacterStat.SetActive(Visible);
        EquipmentScrollView.SetActive(Visible);
        CharacterScrollView.SetActive(!Visible);
    }
}

/*
 * CreateRiskerList�� CreateEquipmentList�� ����� ����� �ϹǷ� �ϳ��� ���� �� ������
 * �̺�Ʈ�����ʿ��� �Ű������� �ѱ�� ListRiskerNumber�� ListEquipmentNumber�� ������ for�� �ȿ��� �����Ǿ�
 * ���� ���������� �Ѿ�µ� �� �κи� �ذ�Ǹ� �ϳ��� ���� ��
 */
