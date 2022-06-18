using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SortManager : MonoBehaviour
{
    public GameObject CharacterScrollView;
    public GameObject EquipmentScrollView;

    public Button[] SortButtons = new Button[3];



    /// <summary>
    /// 1 - 리스커리스트, 2 - 장비리스트
    /// </summary>
    private int ListType = 1;
    /// <summary>
    /// 0 - 클래스, 1 - 등급, 2 - 레벨
    /// </summary>
    private int SortType = 0;

    public void ButtonClickable (bool CharacterInfoManagerState)
    {
        foreach (var SortButton in SortButtons)
        {
            // 1이면 리스커 버튼 생성
            SortButton.interactable = CharacterInfoManagerState;
        }
    }

    // SortButtonPanel의 버튼이 호출
    public void SortList(int Type)
    {
        if (EquipmentScrollView.activeSelf == false)
        {
            // 1-1. 리스커 리스트만 보일 때
            ListType = 1;
            Debug.Log(ListType + " 리스커 리스트를");
        }
        else
        {
            // 1-2. 장비 리스트가 보일 때
            ListType = 2;
            Debug.Log(ListType + " 장비 리스트를");
        }

        // 2. 해당 리스트를
        switch (Type)
        {
            case 0:
                // 클래스 별로 정렬
                Debug.Log("클래스 별로 정렬");
                SortType = 0;
                Sort();
                break;
            case 1:
                // 등급 별로 정렬
                Debug.Log("등급 별로 정렬");
                SortType = 1;
                Sort();
                break;
            case 2:
                // 레벨 별로 정렬
                Debug.Log("레벨 별로 정렬");
                SortType = 2;
                Sort();
                break;
        }
    }

    // 걍 임시로 만든 함수
    private void Sort()
    {
        //CharacterScrollView.transform.GetChild(0).GetChild(0).GetChild(0).SetSiblingIndex(4);
        //Debug.Log("asdasdas  " + CharacterScrollView.transform.GetChild(0).GetChild(0).childCount);
    }

    public void InsertNewButton()
    {
        Transform Content = EquipmentScrollView.transform.GetChild(0).GetChild(0);
        int TemInt = int.Parse(string.Format("{0}", Content.GetChild(Content.childCount - 1).GetComponentInChildren<TextMeshProUGUI>().text));

        switch (SortType)
        {
            case 0:
                for (int i = 0; i < Content.childCount - 1; i++)
                {
                    // EquipmentNumber를 int로 변환하여 대소를 비교
                    if(TemInt < int.Parse(string.Format("{0}", Content.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text)))
                    {
                        Content.GetChild(Content.childCount - 1).SetSiblingIndex(i);
                        break;
                    }
                }
                break;
            case 1:

                break;
            case 2:

                break;
        }
    }
}
/*
 *  유저가 선택한 순서로 정렬 < 유저가 선택한 순서에서 동일한 값이 있을 경우 Number순으로 정렬
 */