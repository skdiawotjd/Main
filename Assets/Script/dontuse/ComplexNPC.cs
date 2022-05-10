using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComplexNPC : MonoBehaviour, IPointerClickHandler
{
    public Text SpeechBubbleText;
    public Image SpeechBubbleImage;

    public GameObject CloseMenu;
    public string InteractionName;

    private string Serihu = "안농";

    void Start()
    {
        StartCoroutine("StartTimerSerihu");
    }

    IEnumerator StartTimerSerihu()
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

    private void StopTimerSerihu()
    {
        StopCoroutine("StartTimerSerihu");

        SpeechBubbleText.gameObject.SetActive(false);
        SpeechBubbleImage.gameObject.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        for(int i = 0; i < transform.childCount; i++)
        {
            if (CloseMenu.transform.GetChild(i).name == InteractionName)
            {
                // 해당 UI 활성화
                CloseMenu.SetActive(true);
                CloseMenu.transform.GetChild(i).gameObject.SetActive(true);

                // 캐릭터 대사 비활성화
                StopTimerSerihu();
            }
        }
    }
}