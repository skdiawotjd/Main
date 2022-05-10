using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TalkManager : MonoBehaviour
{
    List<Dictionary<string, object>> QCell; // 엑셀 파일 csv로 만들어 불러온 것
    Dictionary<int, TalkData> talkData;
    string questName; // 현재 퀘스트 이름
    List<string> talkList; // 대사 리스트
    List<string> talkerList; // talker 리스트
    int talkIndex; // do while문에 사용할 인덱스
    int talkingIndex = 0; // talking에 사용할 인덱스
    int playerIndex; // 플레이어가 가진 현재 퀘스트의 인덱스
    TalkData dataTmp;
    bool myCoroutine = false;

    string s_text;
    public TextMeshProUGUI scriptText;
    public TextMeshProUGUI talkerText;
    

    void Start()
    {
        QCell = CSVReader.Read("MainQData"); // csv 파일 불러오기
        talkList = new List<string>();
        talkerList = new List<string>();
        talkData = new Dictionary<int, TalkData>();
        playerIndex = 10; // 일시적인 인덱스 하드코딩
        GenerateData(playerIndex);
        Next();
    }

    public int GenerateData(int index) // 엑셀에서 불러온 데이터를 리스트로 생성
    {
        for (int i=0;i < QCell.Count;i++)
        {
            if(int.Parse(QCell[i]["QuestIndex"].ToString()) == index) // 매개변수로 받은 인덱스와 퀘스트 인덱스가 일치 할 때
            {
                talkIndex = i; // for문의 현재 인덱스를 do while문에 사용할 인덱스에 저장
                questName = QCell[talkIndex]["QuestName"].ToString(); // 퀘스트네임 저장
                while (int.Parse(QCell[talkIndex]["QuestIndex"].ToString()) != 99) // questIndex가 99가 아닐 때, 즉 한 퀘스트의 대사가 끝날 때 까지
                {
                    talkerList.Add(QCell[talkIndex]["TalkerName"].ToString()); // talkerName을 talker 리스트로
                    talkList.Add(QCell[talkIndex]["QuestScript"].ToString()); // questScript를 talk 리스트로
                    talkIndex++; // index 증가
                } 
                talkData.Add(index, new TalkData(questName, talkerList, talkList)); // 한 퀘스트의 이름과 대사를 TalkData에 저장
                i = QCell.Count; // i를 QCell의 마지막으로 보내 for문을 바로 종료
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
        // talkList들을 사용하는 부분
    }

    public void Next()
    {
        // 코루틴 함수가 실행중인지를 파악하는 기능을 이용해 실행중이라면 텍스트가 한번에 쫙 나오게 
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
