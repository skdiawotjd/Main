using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject talkManager;
    void Start()
    {
        
    }
    public void OnPointerDown(PointerEventData data) {

    }
    public void OnPointerUp(PointerEventData data)
    {
        talkManager.GetComponent<TalkManager>().Next();
    }
}
