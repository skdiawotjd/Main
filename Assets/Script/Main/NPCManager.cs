using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPCManager : MonoBehaviour//, IPointerClickHandler
{
    //private float MaxDistance;
    //private float MinDistance;

    // Count Npc
    public int SimpleNPCMaxCount;
    private int SimpleNPCCurrentCount;

    // Count Position of Npc
    public int SpawnPosCount;
    private int SpawnPosCurrentCount;
    public Vector2[] SpawnPosition = new Vector2[9];
    private int RemovePositionNumber;
    private Vector2[] RemovePosition = new Vector2[9];

    // Prefab if Npc
    private SimpleNPC[] SimpleNPCs = new SimpleNPC[5];
    public SimpleNPC PrefabSimpleNPC;

    // Coroutine
    private WaitForSecondsRealtime SpawnTime;
    private bool IsStop = true;


    void Start()
    {
        SimpleNPCCurrentCount = 0;

        RemovePosition[0] = new Vector2(-2250, -891);
        RemovePosition[1] = new Vector2(-880, -891);
        RemovePosition[2] = new Vector2(-580, -891);
        RemovePosition[3] = new Vector2(-290, -891);
        RemovePosition[4] = new Vector2(10, -891);
        RemovePosition[5] = new Vector2(280, -891);
        RemovePosition[6] = new Vector2(560, -891);
        RemovePosition[7] = new Vector2(870, -891);
        RemovePosition[8] = new Vector2(2250, -891);

        StartCoroutine("SpawnNPC");
    }

    IEnumerator SpawnNPC()
    {
        IsStop = false;

        // NPC Spawn
        while (SimpleNPCCurrentCount < SimpleNPCMaxCount)
        {
            // Wait A Certain Amount Of Time
            SpawnTime = new WaitForSecondsRealtime((float)Random.Range(1, 2));
            yield return SpawnTime;

            SetSpawnPosition();
            SimpleNPCs[SimpleNPCCurrentCount] = Instantiate(PrefabSimpleNPC, SpawnPosition[SpawnPosCurrentCount], Quaternion.identity, gameObject.transform);
            SimpleNPCs[SimpleNPCCurrentCount].SpawnNum = SimpleNPCCurrentCount;
            SimpleNPCs[SimpleNPCCurrentCount].RemovePos = RemovePosition[RemovePositionNumber].x;
            SimpleNPCCurrentCount++;

            Debug.Log("Sapwn 위치" + SpawnPosition[SpawnPosCurrentCount] + " 삭제 위치 " + RemovePosition[RemovePositionNumber].x);
        }

        IsStop = true;
    }

    public void SetSpawnPosition()
    {
        SpawnPosCurrentCount = Random.Range(0, SpawnPosCount);
        RemovePositionNumber = Random.Range(0, RemovePosition.Length);
    }

    public void ClearNPC(int SpawnNum)
    {
        SimpleNPCCurrentCount--;
        SimpleNPCs[SpawnNum] = null;


        if (IsStop && SimpleNPCCurrentCount < SimpleNPCMaxCount)
        {
            StartCoroutine("SpawnNPC");
        }
    }
/*    public void OnPointerClick(PointerEventData eventData)
    {
        sss.text = "눌림";
        //throw new System.NotImplementedException();
    }*/
}