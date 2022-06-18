using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoManager : MonoBehaviour
{
    private UserInfoManager UserInfoManager;

    // ����Ʈ ����
    public SortManager SortManager;
    private bool _ready;
    public bool Ready
    {
        set 
        {
            SortManager.ButtonClickable(value);
            _ready = value; 
        }

        get
        {
            return _ready;
        }
    }

    // ����Ŀ ����Ʈ�� ������ �θ� ������Ʈ
    public GameObject RiskerContent;
    // ��� ����Ʈ�� ������ �θ� ������Ʈ
    public GameObject EquipmentContent;

    // ���¿� ���� �����ִ� ���� ��ȭ��ų ������Ʈ
    public GameObject CharacterStat;
    public GameObject CharacterScrollView;
    public GameObject EquipmentScrollView;
    public Button SetButton;

    // ������ ������ ���� �̹��� ��� ������Ʈ
    public GameObject Variation;
    private Image[] VariationGroup;
    private int[] PreStat;

    // ����Ŀ ����
    public GameObject TextVal;
    private TextMeshProUGUI[] TextValGroup;

    // ���õ� ����Ŀ / ���
    private string SelectedRiskerNumber;
    private string SelectedEquipmentNumber;

    // Ŭ���� ��ư�� �ӽ÷� �����ϴ� ����
    private Button DestroyButton;

    void Start()
    {
        // CharacterInfoManager�� �غ���� �ʾ����Ƿ�
        Ready = false;
        // ���� ������ ����ִ� ������Ʈ ã��
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
        // ���� Variation ���� �ʱ�ȭ
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
        // CharacterInfoManager�� �غ�Ǿ����Ƿ�
        Ready = true;
    }


    private void CreateRiskerList()
    {
        foreach (var Risker in UserInfoManager.UserInfo.RiskerDictionary)
        {
            // 1�̸� ����Ŀ ��ư ����
            CreateButton(1, Risker.Value.RiskerNumber);
        }
    }

    private void CreateEquipmentList()
    {
        foreach (var Equipment in UserInfoManager.UserInfo.EquipmentDictionary)
        {
            // �ش� ��� ������ ĳ���Ͱ� ���� ��
            if (Equipment.Value.OwerNumber == "0")
            {
                // 2�̸� ��� ��ư ����
                CreateButton(2, Equipment.Value.EquipmentNumber);
            }
        }
    }

    /// <summary>
    /// ButtonType : 1 - ����Ŀ, 2 - ���
    /// </summary> 
    private void CreateButton(int ButtonType, string Number)
    {
        switch (ButtonType)
        {
            case 1:
                // ����Ŀ ��ư ����
                GameObject RiskerButton = Instantiate(Resources.Load("RiskerButton"), RiskerContent.transform) as GameObject;

                RiskerButton.GetComponentInChildren<TextMeshProUGUI>().text = Number;

                RiskerButton.GetComponent<Button>().onClick.AddListener(() => { ClickRisker(Number); });
                // ����Ŀ ��ư�� �����ϱ� ���� RiskerNumber�� ��ư �̸����� ����
                RiskerButton.name = Number;
                // ������ ��ư�� SortManager�� ����Ʈ�� �����ϱ�

                break;
            case 2:
                // ��� ��ư ����
                GameObject EquipmentButton = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;

                EquipmentButton.GetComponentInChildren<TextMeshProUGUI>().text = Number;

                EquipmentButton.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(Number, EquipmentButton.GetComponent<Button>()); });
                // ��� ��ư�� �����ϱ� ���� EquipmentNumber�� ��ư �̸����� ����
                EquipmentButton.name = Number;
                // ������ ��ư�� SortManager�� ����Ʈ�� �����ϱ�

                break;
        }
    }

    private void ClickRisker(string RiskerNumber)
    {
        // ���ϰ� �Ⱥ��ϰ� ����
        SetActiveObject(true);
        // ���� ��ư ��Ȱ��ȭ
        SetButton.gameObject.SetActive(false);
        // ���õ� RiskerNumber�� ����
        SelectedRiskerNumber = RiskerNumber;
        // Variation �ʱ�ȭ
        InitializeVariation();
        // ����Ŀ�� �⺻ ���� ���
        UpdateRiskerStat(SelectedRiskerNumber);
        // ����Ŀ�� ������ ��� ������ ��� ���� ���ϱ�
        for (int i = 0; i < UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber.Length; i++)
        {
            if (UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i] != "0")
            {
                // ���õ� EquipmentNumber�� ����
                SelectedEquipmentNumber = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i];
                UpdateEquipmentStat();
            }
        }
        // �������� Stat�� PreStat�� ����
        SetRiskerStat();
    }
    private void ClickEquipment(string EquipmentNumber, Button ClickedButton)
    {
        // Variation �ʱ�ȭ
        InitializeVariation();
        // �������� ������ ���� ����Ŀ�� �������� ����
        InitializeRiskerStat();

        SelectedEquipmentNumber = EquipmentNumber;

        Debug.Log("�ش� ����� �ѹ� " + SelectedEquipmentNumber);
        Debug.Log("�ش� ����� ���� " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);

        UpdateEquipmentStat();
        UpdateVariation(UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].StatOrder);

        // ��� ��ư Ŭ�� �� ���� ��ư Ȱ��ȭ
        SetButton.gameObject.SetActive(true);
        // ���õ� ��� ��ư�� ���Ŀ� �����ϱ� ���� �ش� ��ư�� ����
        DestroyButton = ClickedButton;
    }

    // ������ư�� ȣ��
    public void SetEquipment()
    {
        // 1. ���� ��ư ��Ȱ��ȭ
        SetButton.gameObject.SetActive(false);

        // 2. ������ ��ư�� ����Ʈ���� ����
        DestroyButton.transform.SetParent(null, false);
        
        // 3-1. ������ ��� ������ ������ ��� ������ �����ϰ� �ִ��� Ȯ��
        Debug.Log("���õ� ����Ŀ�� ActiveEquipmentNumber[" + UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[((SelectedEquipmentNumber[0] - '0') - 1)] + "]");
        if (UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[((SelectedEquipmentNumber[0] - '0') - 1)] != "0")
        {
            // 3-1-1. �����ϰ� �ִٸ� ���ο� ��� ��ư ����
            CreateButton(2, UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[((SelectedEquipmentNumber[0] - '0') - 1)]);
            // 3-1-2. ���� ������ ����� OwnerNumber �ʱ�ȭ 
            UserInfoManager.UserInfo.EquipmentDictionary[UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[((SelectedEquipmentNumber[0] - '0') - 1)]].OwerNumber = "0";
            // 3-1-3. ��� ����Ʈ ����
            SortManager.InsertNewButton();
        }
        // 3-2. ���ٸ� �Ѿ

        // 4. ���� ������ ����� OwnerNumber ����
        UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].OwerNumber = SelectedRiskerNumber;
        // 5. ���õ� ����Ŀ�� ActiveEquipmentNumber�� ����
        UserInfoManager.UserInfo.SetEquipmentToRisker(SelectedRiskerNumber, SelectedEquipmentNumber);

        // 6. �������� Stat�� PreStat�� ����
        SetRiskerStat();
        // 7. Variation�� ������� set
        InitializeVariation();
        // 8. Ŭ���� ��ư ����
        Destroy(DestroyButton.gameObject);
    }

    private void UpdateRiskerStat(string ListRiskerNumber)
    {
        for (int i = 0; i < 6; i++)
        {
            TextValGroup[i].text = UserInfoManager.UserInfo.RiskerStat(ListRiskerNumber)[i].ToString();
        }
    }

    private void UpdateEquipmentStat()
    {
        Debug.Log("������ ��� �ɷ�ġ " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
        // �ش� ����� StatOrder�� ���� ��� �������� Ȯ��
        switch (UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].StatOrder)
        {
            case 1:
                TextValGroup[1].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[1] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
                break;
            case 2:
                TextValGroup[2].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[2] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
                break;
            case 3:
                TextValGroup[3].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[3] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
                TextValGroup[4].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[4] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
                break;
            case 4:
                TextValGroup[5].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[5] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
                break;
        }
    }

    private void UpdateVariation(int StatOrder)
    {
        Debug.Log(TextValGroup[StatOrder].text + " - " + PreStat[StatOrder - 1].ToString());
        if (int.Parse(string.Format("{0}", TextValGroup[StatOrder].text)) - PreStat[StatOrder - 1] > 0)
        {
            Debug.Log("����");
            VariationGroup[StatOrder - 1].color = new Color32(255, 0, 0, 255);
        }
        else if (int.Parse(string.Format("{0}", TextValGroup[StatOrder].text)) - PreStat[StatOrder - 1] == 0)
        {
            Debug.Log("�״��");
            VariationGroup[StatOrder - 1].color = new Color32(255, 255, 255, 255);
        }
        else if (int.Parse(string.Format("{0}", TextValGroup[StatOrder].text)) - PreStat[StatOrder - 1] < 0)
        {
            Debug.Log("����");
            VariationGroup[StatOrder - 1].color = new Color32(0, 0, 255, 255);
        }
    }

    private void InitializeVariation()
    {
        for(int i = 0; i < VariationGroup.Length; i++)
        {
            VariationGroup[i].color = new Color32(255, 255, 255, 255);
        }
    }

    private void InitializeRiskerStat()
    {
        for (int i = 0; i < PreStat.Length; i++)
        {
            // TextValGroup[0].text�� �����̹Ƿ� ������ �ʿ䰡 ����
            TextValGroup[i + 1].text = PreStat[i].ToString();
        }
    }

    private void SetRiskerStat()
    {
        for (int i = 0; i < PreStat.Length; i++)
        {
            // TextValGroup[0].text�� �����̹Ƿ� ������ �ʿ䰡 ����
            PreStat[i] = int.Parse(string.Format("{0}", TextValGroup[i + 1].text));
        }
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
 *  �̺�Ʈ�����ʿ��� �Ű������� �ѱ�� ListRiskerNumber�� ListEquipmentNumber�� ������ for�� �ȿ��� �����Ǿ�
 *  ���� ���������� �Ѿ�µ� �� �κи� �ذ�Ǹ� �ϳ��� ���� ��
 *  
 * EquipmentNumber�� ����/�� �� ������ ������ ���ε� �̸� ����
 * � Stat������ �����ϰ� �־� �̸� �и��� �ʿ䰡 ����
 * 
 * if (SelectedEquipmentNumber < 1000)�ϴ� �κ��� ��� ���� ��
 * Ư�� ������ �����ϰ� �̸� ��� �̿��ϴ� ���� ������ [ UpdateEquipmentStat() ]
 */
