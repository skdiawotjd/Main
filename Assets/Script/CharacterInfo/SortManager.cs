using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    public GameObject RiskerContent;
    public GameObject EquipmentContent;
    
    // 1 - 리스커리스트, 2 - 장비리스트
    private int ListType = 1;

    void Start()
    {
        
    }

    // SortButtonPanel의 버튼이 호출
    public void SortList(int Type)
    {
        if(EquipmentContent.activeSelf == false)
        {
            // 1-1. 리스커 리스트만 보일 때
            ListType = 1;
        }
        else
        {
            // 1-2. 장비 리스트가 보일 때
            ListType = 2;
        }

        // 2. 해당 리스트를
        switch (Type)
        {
            case 0:
                // 등급 별로 정렬
                Sort();
                break;
            case 1:
                // 클래스 별로 정렬
                Sort();
                break;
            case 2:
                // 레벨 별로 정렬
                Sort();
                break;
        }
    }

    // 걍 임시로 만든 함수
    private void Sort()
    {
        if(ListType == 0)
        {
            Debug.Log("리스커 리스트 정렬");
        }
        else if(ListType == 1)
        {
            Debug.Log("장비 리스트 정렬");
        }
    }
}
/*
 * 기본적으로는 RiskerNumber/EquipnemtNumber 순으로 정렬
 */