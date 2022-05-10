using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TalkManager : MonoBehaviour
{
    List<Dictionary<string, object>> QCell; // ���� ���� csv�� ����� �ҷ��� ��
    Dictionary<int, TalkData> talkData;
    string questName; // ���� ����Ʈ �̸�
    List<string> talkList; // ��� ����Ʈ
    List<string> talkerList; // talker ����Ʈ
    int talkIndex; // do while���� ����� �ε���
    int talkingIndex = 0; // talking�� ����� �ε���
    int playerIndex; // �÷��̾ ���� ���� ����Ʈ�� �ε���
    TalkData dataTmp;
    bool myCoroutine = false;

    string s_text;
    public TextMeshProUGUI scriptText;
    public TextMeshProUGUI talkerText;
    

    void Start()
    {
        QCell = CSVReader.Read("MainQData"); // csv ���� �ҷ�����
        talkList = new List<string>();
        talkerList = new List<string>();
        talkData = new Dictionary<int, TalkData>();
        playerIndex = 10; // �Ͻ����� �ε��� �ϵ��ڵ�
        GenerateData(playerIndex);
        Next();
    }

    public int GenerateData(int index) // �������� �ҷ��� �����͸� ����Ʈ�� ����
    {
        for (int i=0;i < QCell.Count;i++)
        {
            if(int.Parse(QCell[i]["QuestIndex"].ToString()) == index) // �Ű������� ���� �ε����� ����Ʈ �ε����� ��ġ �� ��
            {
                talkIndex = i; // for���� ���� �ε����� do while���� ����� �ε����� ����
                questName = QCell[talkIndex]["QuestName"].ToString(); // ����Ʈ���� ����
                while (int.Parse(QCell[talkIndex]["QuestIndex"].ToString()) != 99) // questIndex�� 99�� �ƴ� ��, �� �� ����Ʈ�� ��簡 ���� �� ����
                {
                    talkerList.Add(QCell[talkIndex]["TalkerName"].ToString()); // talkerName�� talker ����Ʈ��
                    talkList.Add(QCell[talkIndex]["QuestScript"].ToString()); // questScript�� talk ����Ʈ��
                    talkIndex++; // index ����
                } 
                talkData.Add(index, new TalkData(questName, talkerList, talkList)); // �� ����Ʈ�� �̸��� ��縦 TalkData�� ����
                i = QCell.Count; // i�� QCell�� ���������� ���� for���� �ٷ� ����
            }
            
        }
        return 0;
    }

    public void Talking()
    {

        //talkData.TryGetValue(playerIndex, out dataTmp);
        talkerText.text = dataTmp.talkerName[talkingIndex];
        s_text = dataTmp.scripts[talkingIndex];
        StartCoroutine("_typing");
        // talkList���� ����ϴ� �κ�
    }

    public void Next()
    {
        // �ڷ�ƾ �Լ��� ������������ �ľ��ϴ� ����� �̿��� �������̶�� �ؽ�Ʈ�� �ѹ��� �� ������ 
        if(myCoroutine)
        {
            StopCoroutine("_typing");
            scriptText.text = s_text;
            myCoroutine = false;
        }
        else
        {
            talkData.TryGetValue(playerIndex, out dataTmp);
            
            Debug.Log(talkingIndex + " > " + dataTmp.talkerName.Count);
            if ((talkingIndex) == dataTmp.talkerName.Count)
            {
                talkingIndex = 0;
                End();
            }
            else
            {
                Talking();
                talkingIndex++;
            }
        }
    }
    public void End()
    {
        SceneManager.LoadScene("Bar");
    }

    IEnumerator _typing()
    {
        myCoroutine = true;
        for(int i=0;i <= s_text.Length;i++)
        {
            scriptText.text = s_text.Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }
        myCoroutine = false;
    }
}
