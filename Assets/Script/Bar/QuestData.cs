using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    public string questName;
    public string questContents;
    public int questRewards;
    public int questProcessivity;
    public int questCount;
    public int[] npcId;

    public QuestData(string name, string contents, int rewards, int processivity, int count)
    {
        questName = name;
        questContents = contents;
        questRewards = rewards;
        questProcessivity = processivity;
        questCount = count;
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
