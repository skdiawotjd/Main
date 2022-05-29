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

    // Ŭ���� ��ư�� �ӽ÷� �����ϴ� ����
    private Button DestroyButton;

    void Start()
    {
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
    }


    private void CreateRiskerList()
    {
        foreach (var Risker in UserInfoManager.UserInfo.RiskerDictionary)
        {
            // 0�̸� ����Ŀ ��ư ����
            CreateButton(0, Risker.Value.RiskerNumber);
        }

    }

    private void CreateEquipmentList()
    {
        foreach (var Equipment in UserInfoManager.UserInfo.EquipmentDictionary)
        {
            // �ش� ��� ������ ĳ���Ͱ� ���� ��
            if (Equipment.Value.OwerNumber == -1)
            {
                // 1�̸� ��� ��ư ����
                CreateButton(1, Equipment.Value.EquipmentNumber);
            }
        }
    }

    private void CreateButton(int Order, int Number)
    {
        switch (Order)
        {
            case 0:
                // ����Ŀ ��ư ����
                GameObject RiskerButton = Instantiate(Resources.Load("RiskerButton"), RiskerContent.transform) as GameObject;
                RiskerButton.GetComponent<Button>().onClick.AddListener(() => { ClickRisker(Number); });
                // ������ ��ư�� SortManager�� ����Ʈ�� �����ϱ�

                break;
            case 1:
                // ��� ��ư ����
                GameObject EquipmentButton = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
                EquipmentButton.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(Number, EquipmentButton.GetComponent<Button>()); });
                // ������ ��ư�� SortManager�� ����Ʈ�� �����ϱ�

                break;
        }
    }

    private void ClickRisker(int RiskerNumber)
    {
        // ���ϰ� �Ⱥ��ϰ� ����
        SetActiveObject(true);
        // ���õ� RiskerNumber�� ����
        SelectedRiskerNumber = RiskerNumber;
        // Variation �ʱ�ȭ
        InitializeVariation();
        // ����Ŀ�� �⺻ ���� ���
        UpdateRiskerStat(SelectedRiskerNumber);
        // ����Ŀ�� ������ ��� ������ ��� ���� ���ϱ�
        for (int i = 0; i < UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber.Length; i++)
        {
            if (UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i] != -1)
            {
                // ���õ� EquipmentNumber�� ����
                SelectedEquipmentNumber = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i];
                UpdateEquipmentStat();
            }
        }
        // �������� Stat�� PreStat�� ����
        SetRiskerStat();
    }
    private void ClickEquipment(int EquipmentNumber, Button ClickedButton)
    {
        SelectedEquipmentNumber = EquipmentNumber;

        Debug.Log("�ش� ����� �ѹ� " + SelectedEquipmentNumber);
        Debug.Log("�ش� ����� ���� " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);

        UpdateEquipmentStat();
        UpdateVariation(CheckEquipmentType());

        // ���õ� ��� ��ư�� ���Ŀ� �����ϱ� ���� �ش� ��ư�� ����
        DestroyButton = ClickedButton;
    }

    // ������ư�� ȣ��
    public void SetEquipment()
    {
        // 1. ������ ��� ������ ������ ��� ������ �����ϰ� �ִ��� Ȯ��
        if (UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[CheckEquipmentType()] != -1)
        {
            // 2-1. �����ϰ� �ִٸ� ���ο� ��� ��ư ����
            CreateButton(1, UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[CheckEquipmentType()]);
            // 2-2. ���� ������ ����� OwnerNumber �ʱ�ȭ 
            UserInfoManager.UserInfo.EquipmentDictionary[UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[CheckEquipmentType()]].OwerNumber = -1;
        }
        // 2. ���ٸ� �Ѿ��
        // 3. ���� ������ ����� OwnerNumber ����
        UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].OwerNumber = SelectedRiskerNumber;
        // 4. ����Ŀ�� ActiveEquipmentNumber�� ����
        UserInfoManager.UserInfo.SetEquipmentToRisker(SelectedRiskerNumber, SelectedEquipmentNumber);
        // 5. ��� ����Ʈ ����

        // 6. �������� Stat�� PreStat�� ����
        SetRiskerStat();
        // 7. Variation�� ������� set
        InitializeVariation();
        // 8. ������ ��ư ����
        Destroy(DestroyButton.gameObject);
    }

    private void UpdateRiskerStat(int ListRiskerNumber)
    {
        for (int i = 0; i < 6; i++)
        {
            TextValGroup[i].text = UserInfoManager.UserInfo.RiskerStat(ListRiskerNumber)[i].ToString();
        }
    }

    private int CheckEquipmentType()
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
        Debug.Log("�ɷ�ġ " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
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
        for(int i = 0; i < VariationGroup.Length; i++)
        {
            VariationGroup[i].color = new Color32(255, 255, 255, 255);
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
