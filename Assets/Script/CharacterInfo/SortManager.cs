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
    /// 1 - ����Ŀ����Ʈ, 2 - ��񸮽�Ʈ
    /// </summary>
    private int ListType = 1;
    /// <summary>
    /// 0 - Ŭ����, 1 - ���, 2 - ����
    /// </summary>
    private int SortType = 0;

    public void ButtonClickable (bool CharacterInfoManagerState)
    {
        foreach (var SortButton in SortButtons)
        {
            // 1�̸� ����Ŀ ��ư ����
            SortButton.interactable = CharacterInfoManagerState;
        }
    }

    // SortButtonPanel�� ��ư�� ȣ��
    public void SortList(int Type)
    {
        if (EquipmentScrollView.activeSelf == false)
        {
            // 1-1. ����Ŀ ����Ʈ�� ���� ��
            ListType = 1;
            Debug.Log(ListType + " ����Ŀ ����Ʈ��");
        }
        else
        {
            // 1-2. ��� ����Ʈ�� ���� ��
            ListType = 2;
            Debug.Log(ListType + " ��� ����Ʈ��");
        }

        // 2. �ش� ����Ʈ��
        switch (Type)
        {
            case 0:
                // Ŭ���� ���� ����
                Debug.Log("Ŭ���� ���� ����");
                SortType = 0;
                Sort();
                break;
            case 1:
                // ��� ���� ����
                Debug.Log("��� ���� ����");
                SortType = 1;
                Sort();
                break;
            case 2:
                // ���� ���� ����
                Debug.Log("���� ���� ����");
                SortType = 2;
                Sort();
                break;
        }
    }

    // �� �ӽ÷� ���� �Լ�
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
                    // EquipmentNumber�� int�� ��ȯ�Ͽ� ��Ҹ� ��
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
 *  ������ ������ ������ ���� < ������ ������ �������� ������ ���� ���� ��� Number������ ����
 */