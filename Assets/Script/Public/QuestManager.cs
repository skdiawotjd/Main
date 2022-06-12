using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public UserInfoManager UserInfoManager;
    /// <summary>
    /// 리스트 0번 : 메인퀘스트
    /// 리스트 1번 : 일일퀘스트
    /// 리스트 2번 : 주간퀘스트
    /// 리스트 3번 : 긴급퀘스트
    /// </summary>
    private List<QuestInfo> QuestList;
    
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    
    private void CreateQuestInfo()
    {

    }
}
