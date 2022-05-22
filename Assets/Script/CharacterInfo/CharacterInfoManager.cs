using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterInfoManager : MonoBehaviour
{
    private UserInfoManager UserInfoManager;

    // 리스커 리스트를 생성할 부모 오브젝트
    public GameObject RiskerContent;
    // 장비 리스트를 생성할 부모 오브젝트
    public GameObject EquipmentContent;

    // 상태에 따라 보여주는 것을 변화시킬 오브젝트
    public GameObject CharacterStat;
    public GameObject CharacterScrollView;
    public GameObject EquipmentScrollView;

    // 스탯의 증감에 따른 이미지 출력 오브젝트
    public GameObject Variation;
    private Image[] VariationGroup;
    private int[] PreStat;

    // 리스커 스탯
    public GameObject TextVal;
    private TextMeshProUGUI[] TextValGroup;

    // 선택된 리스커 / 장비
    private int SelectedRiskerNumber = -1;
    private int SelectedEquipmentNumber = -1;

    void Start()
    {
        UserInfoManager = GameObject.Find("UserInfoManager").GetComponent<UserInfoManager>();
        // 리스커 리스트 생성
        CreateRiskerList();
        // 장비 리스트 생성
        CreateEquipmentList();
        // 리스커 스탯 변수 초기화
        TextValGroup = new TextMeshProUGUI[6];
        for (int i = 0; i < TextValGroup.Length; i++)
        {
            TextValGroup[i] = TextVal.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
        // 스탯 증감 확인용 변수 초기화
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
        // 보일거 안보일거 설정
        SetActiveObject(true);
        SelectedRiskerNumber = RiskerNumber;
        // 증감을 흰색으로 set
        InitializeVariation();

        // 첫 RiskerStat을 Initialize
        // 1. 리스커의 기본 스탯 출력
        UpdateRiskerStat(SelectedRiskerNumber);
        // 2. 리스커에 장착된 장비가 있으면 장비 스탯 더하기

        for (int i = 0; i < UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber.Length; i++)
        {
            if (UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i] != -1)
            {
                SelectedEquipmentNumber = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i];
                UpdateEquipmentStat();
            }
        }
        // 3. 최종적인 Stat을 PreStat에 저장
        SetRiskerStat();
    }
    private void ClickEquipment(int EquipmentNumber)
    {
        SelectedEquipmentNumber = EquipmentNumber;
        //UserInfoManager.SetChangeEquipment(SelectedRiskerNumber, EquipmentNumber);
        Debug.Log("해당 장비의 넘버 " + EquipmentNumber);
        Debug.Log("해당 장비의 스탯 " + UserInfoManager.UserInfo.EquipmentDictionary[EquipmentNumber].Stat);


        UpdateEquipmentStat();
        UpdateVariation(CheckEquipmentNumber());
        //asd asd
    }


    public void SetEquipment()
    {
        GameObject btn = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
        int tem = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[CheckEquipmentNumber()];
        btn.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(tem); });
        // 리스커에 ActiveEquipmentNumber를 갱신
        UserInfoManager.UserInfo.SetEquipmentToRisker(SelectedRiskerNumber, SelectedEquipmentNumber);
        // 장비 리스트 갱신

        // 최종적인 Stat을 PreStat에 저장
        SetRiskerStat();
        // 증감을 흰색으로 set
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
            // 이전 스탯과 현재 스탯을 저장
            Debug.Log("능력치 " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
            //
            //PreStat[0] = int.Parse(string.Format("{0}", TextValGroup[1].text));
            //
            TextValGroup[1].text = (UserInfoManager.UserInfo.RiskerStat(SelectedRiskerNumber)[1] + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat).ToString();
            /*// 저장된 스탯가지고 증감 확인
            UpdateVariation(0);
            // 새 버튼 생성
            GameObject btn = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
            int tem = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[0];
            btn.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(tem); });
            // 바뀐 장비 Rikser에 저장
            SetEquipment();*/
        }
        else if (SelectedEquipmentNumber < 2000)
        {
            Debug.Log("능력치 " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
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
            Debug.Log("능력치 " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
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
            Debug.Log("능력치 " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
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
            Debug.Log("증가");
            VariationGroup[StatOrder].color = new Color32(255, 0, 0, 255);
        }
        else if (int.Parse(string.Format("{0}", TextValGroup[StatOrder + 1].text)) - PreStat[StatOrder] == 0)
        {
            Debug.Log("그대로");
            VariationGroup[StatOrder].color = new Color32(255, 255, 255, 255);
        }
        else if (int.Parse(string.Format("{0}", TextValGroup[StatOrder + 1].text)) - PreStat[StatOrder] < 0)
        {
            Debug.Log("감소");
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

    // 3. 최종적인 Stat을 Risker의 Stat에 저장
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
 * CreateRiskerList와 CreateEquipmentList는 비슷한 기능을 하므로 하나로 묶을 수 있을듯
 * 이벤트리스너에서 매개변수로 넘기는 ListRiskerNumber와 ListEquipmentNumber가 무조건 for문 안에서 생성되야
 * 값이 정상적으로 넘어가는데 이 부분만 해결되먼 하나로 묶일 듯
 */
