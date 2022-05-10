using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkData : MonoBehaviour
{
    public string questName;
    public List<string> talkerName;
    public List<string> scripts;

    public TalkData(string QuestName, List<string> TalkerName, List<string> Scripts)
    {
        questName = QuestName;
        talkerName = TalkerName;
        scripts = Scripts;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
