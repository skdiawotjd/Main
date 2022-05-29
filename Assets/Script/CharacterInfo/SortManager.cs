using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    public GameObject RiskerContent;
    public GameObject EquipmentContent;
    
    // 1 - ����Ŀ����Ʈ, 2 - ��񸮽�Ʈ
    private int ListType = 1;

    void Start()
    {
        
    }

    // SortButtonPanel�� ��ư�� ȣ��
    public void SortList(int Type)
    {
        if(EquipmentContent.activeSelf == false)
        {
            // 1-1. ����Ŀ ����Ʈ�� ���� ��
            ListType = 1;
        }
        else
        {
            // 1-2. ��� ����Ʈ�� ���� ��
            ListType = 2;
        }

        // 2. �ش� ����Ʈ��
        switch (Type)
        {
            case 0:
                // ��� ���� ����
                Sort();
                break;
            case 1:
                // Ŭ���� ���� ����
                Sort();
                break;
            case 2:
                // ���� ���� ����
                Sort();
                break;
        }
    }

    // �� �ӽ÷� ���� �Լ�
    private void Sort()
    {
        if(ListType == 0)
        {
            Debug.Log("����Ŀ ����Ʈ ����");
        }
        else if(ListType == 1)
        {
            Debug.Log("��� ����Ʈ ����");
        }
    }
}
/*
 * �⺻�����δ� RiskerNumber/EquipnemtNumber ������ ����
 */