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

    // 클릭한 버튼을 임시로 저장하는 변수
    private Button DestroyButton;

    void Start()
    {
        // 유저 정보를 담고있는 오브젝트 찾기
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
        // 스탯 Variation 변수 초기화
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
            // 0이면 리스커 버튼 생성
            CreateButton(0, Risker.Value.RiskerNumber);
        }

    }

    private void CreateEquipmentList()
    {
        foreach (var Equipment in UserInfoManager.UserInfo.EquipmentDictionary)
        {
            // 해당 장비를 소유한 캐릭터가 없을 때
            if (Equipment.Value.OwerNumber == -1)
            {
                // 1이면 장비 버튼 생성
                CreateButton(1, Equipment.Value.EquipmentNumber);
            }
        }
    }

    private void CreateButton(int Order, int Number)
    {
        switch (Order)
        {
            case 0:
                // 리스커 버튼 생성
                GameObject RiskerButton = Instantiate(Resources.Load("RiskerButton"), RiskerContent.transform) as GameObject;
                RiskerButton.GetComponent<Button>().onClick.AddListener(() => { ClickRisker(Number); });
                // 생성한 버튼을 SortManager의 리스트에 저장하기

                break;
            case 1:
                // 장비 버튼 생성
                GameObject EquipmentButton = Instantiate(Resources.Load("EquipmentButton"), EquipmentContent.transform) as GameObject;
                EquipmentButton.GetComponent<Button>().onClick.AddListener(() => { ClickEquipment(Number, EquipmentButton.GetComponent<Button>()); });
                // 생성한 버튼을 SortManager의 리스트에 저장하기

                break;
        }
    }

    private void ClickRisker(int RiskerNumber)
    {
        // 보일거 안보일거 설정
        SetActiveObject(true);
        // 선택된 RiskerNumber를 저장
        SelectedRiskerNumber = RiskerNumber;
        // Variation 초기화
        InitializeVariation();
        // 리스커의 기본 스탯 출력
        UpdateRiskerStat(SelectedRiskerNumber);
        // 리스커에 장착된 장비가 있으면 장비 스탯 더하기
        for (int i = 0; i < UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber.Length; i++)
        {
            if (UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i] != -1)
            {
                // 선택된 EquipmentNumber를 저장
                SelectedEquipmentNumber = UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[i];
                UpdateEquipmentStat();
            }
        }
        // 최종적인 Stat을 PreStat에 저장
        SetRiskerStat();
    }
    private void ClickEquipment(int EquipmentNumber, Button ClickedButton)
    {
        SelectedEquipmentNumber = EquipmentNumber;

        Debug.Log("해당 장비의 넘버 " + SelectedEquipmentNumber);
        Debug.Log("해당 장비의 스탯 " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);

        UpdateEquipmentStat();
        UpdateVariation(CheckEquipmentType());

        // 선택된 장비 버튼을 추후에 삭제하기 위해 해당 버튼을 저장
        DestroyButton = ClickedButton;
    }

    // 장착버튼이 호출
    public void SetEquipment()
    {
        // 1. 선택한 장비 종류와 동일한 장비 종류를 착용하고 있는지 확인
        if (UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[CheckEquipmentType()] != -1)
        {
            // 2-1. 장착하고 있다면 새로운 장비 버튼 생성
            CreateButton(1, UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[CheckEquipmentType()]);
            // 2-2. 장착 해제된 장비의 OwnerNumber 초기화 
            UserInfoManager.UserInfo.EquipmentDictionary[UserInfoManager.UserInfo.RiskerDictionary[SelectedRiskerNumber].ActiveEquipmentNumber[CheckEquipmentType()]].OwerNumber = -1;
        }
        // 2. 없다면 넘어가고
        // 3. 새로 장착된 장비의 OwnerNumber 변경
        UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].OwerNumber = SelectedRiskerNumber;
        // 4. 리스커에 ActiveEquipmentNumber를 갱신
        UserInfoManager.UserInfo.SetEquipmentToRisker(SelectedRiskerNumber, SelectedEquipmentNumber);
        // 5. 장비 리스트 갱신

        // 6. 최종적인 Stat을 PreStat에 저장
        SetRiskerStat();
        // 7. Variation을 흰색으로 set
        InitializeVariation();
        // 8. 선택한 버튼 삭제
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
        Debug.Log("능력치 " + UserInfoManager.UserInfo.EquipmentDictionary[SelectedEquipmentNumber].Stat);
        // 해당 장비의 StatOrder를 통해 어느 스탯인지 확인
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
        for(int i = 0; i < VariationGroup.Length; i++)
        {
            VariationGroup[i].color = new Color32(255, 255, 255, 255);
        }
    }

    private void SetRiskerStat()
    {
        for (int i = 0; i < PreStat.Length; i++)
        {
            // TextValGroup[0].text는 레벨이므로 변경할 필요가 없음
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
 * CreateRiskerList와 CreateEquipmentList는 비슷한 기능을 하므로 하나로 묶을 수 있을듯
 *  이벤트리스너에서 매개변수로 넘기는 ListRiskerNumber와 ListEquipmentNumber가 무조건 for문 안에서 생성되야
 *  값이 정상적으로 넘어가는데 이 부분만 해결되먼 하나로 묶일 듯
 *  
 * EquipmentNumber는 무기/방어구 등 종류를 구분한 값인데 이를 통해
 * 어떤 Stat인지를 구분하고 있어 이를 분리할 필요가 있음
 * 
 * if (SelectedEquipmentNumber < 1000)하는 부분을 장비 선택 시
 * 특정 변수에 저장하고 이를 계속 이용하는 편이 나을듯 [ UpdateEquipmentStat() ]
 */
