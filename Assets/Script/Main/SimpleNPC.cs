using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleNPC : MonoBehaviour
{
    public Text SpeechBubbleText;
    public Image SpeechBubbleImage;

    private float WalkSpeed = 1.0f;
    private float Direction = 0.0f;

    private string Serihu = "안농";

    public int SpawnNum;
    public float RemovePos;

    void Start()
    {
        StartCoroutine("SetTimerSerihu");
        StartCoroutine("SetTimerMove");
        
    }

    IEnumerator SetTimerSerihu()
    {
        WaitForSecondsRealtime StandbySerihu;
        WaitForSecondsRealtime OutputsSerihu;

        while (true)
        {
            StandbySerihu = new WaitForSecondsRealtime((float)Random.Range(1, 21));
            OutputsSerihu = new WaitForSecondsRealtime((float)Random.Range(3, 6));
            // wait
            yield return StandbySerihu;
            // visible
            SpeechBubbleText.text = Serihu;
            SpeechBubbleText.gameObject.SetActive(true);
            SpeechBubbleImage.gameObject.SetActive(true);
            // wait
            yield return OutputsSerihu;
            // invisible
            SpeechBubbleText.gameObject.SetActive(false);
            SpeechBubbleImage.gameObject.SetActive(false);
        }
    }

    IEnumerator SetTimerMove()
    {
        WaitForSecondsRealtime WaitTime;

        while (true)
        {
            WaitTime = new WaitForSecondsRealtime((float)Random.Range(3, 11));
            Direction = (float)Random.Range(0, 2);

            // wait
            yield return WaitTime;
            // SetDirection
            if (Random.Range(0, 2) == 1)
            {
                Direction = 1.0f;
            }
            else
            {
                Direction = -1.0f;
            }
            // walk
            yield return StartCoroutine("MoveNPC");
        }
    }
    IEnumerator MoveNPC()
    {
        float UseTime = 0.0f;
        float WalkTime = (float)Random.Range(3, 8);
        WaitForSecondsRealtime Tick = new WaitForSecondsRealtime(0.01f);


        while (UseTime < WalkTime)
        {
            //move
            transform.Translate(WalkSpeed * Direction, 0, 0);
            UseTime += 0.01f;

            if((Mathf.Abs(RemovePos) - Mathf.Abs(transform.position.x)) > 0 && (Mathf.Abs(RemovePos) - Mathf.Abs(transform.position.x)) < 9)
            {
                Debug.Log("삭제된 위치 + " + ((int)Mathf.Abs(transform.position.x)));
            }

            if (((int)Mathf.Abs(transform.position.x)) % RemovePos == Mathf.Abs(RemovePos) % RemovePos || Mathf.Abs(transform.position.x) > 2259)
            {
                Debug.Log("삭제된 위치 + " + ((int)Mathf.Abs(transform.position.x)) % 10);
                transform.parent.GetComponent<NPCManager>().ClearNPC(SpawnNum);
                StopCoroutine("MoveNPC");
                StopCoroutine("SetTimerMove");
                Destroy(gameObject);
            }

            yield return Tick;
        }
    }
}


/*
    NPC가 화면 밖을 나가면 사라지게?
    NPC의 스폰을 어떻게 할까

 */