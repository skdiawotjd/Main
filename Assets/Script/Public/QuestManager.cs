using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public UserInfoManager UserInfoManager;
    /// <summary>
    /// ����Ʈ 0�� : ��������Ʈ
    /// ����Ʈ 1�� : ��������Ʈ
    /// ����Ʈ 2�� : �ְ�����Ʈ
    /// ����Ʈ 3�� : �������Ʈ
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
