using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    public GameObject CharacterScrollView;
    public GameObject EquipmentScrollView;
    
    // 1 - ����Ŀ����Ʈ, 2 - ��񸮽�Ʈ
    private int ListType = 1;

    void Start()
    {
        
    }

    // SortButtonPanel�� ��ư�� ȣ��
    public void SortList(int Type)
    {
        Debug.Log("���������������� " + EquipmentScrollView.activeSelf);
        if (EquipmentScrollView.activeSelf == false)
        {
            // 1-1. ����Ŀ ����Ʈ�� ���� ��
            ListType = 1;
            Debug.Log("����Ŀ ����Ʈ��");
        }
        else
        {
            // 1-2. ��� ����Ʈ�� ���� ��
            ListType = 2;
            Debug.Log("��� ����Ʈ��");
        }

        // 2. �ش� ����Ʈ��
        switch (Type)
        {
            case 0:
                // ��� ���� ����
                Debug.Log("��� ���� ����");
                Sort();
                break;
            case 1:
                // Ŭ���� ���� ����
                Debug.Log("Ŭ���� ���� ����");
                Sort();
                break;
            case 2:
                // ���� ���� ����
                Debug.Log("���� ���� ����");
                Sort();
                break;
        }
    }

    // �� �ӽ÷� ���� �Լ�
    private void Sort()
    {
        
    }
}
/*
 * �⺻�����δ� RiskerNumber/EquipnemtNumber ������ ����
 */