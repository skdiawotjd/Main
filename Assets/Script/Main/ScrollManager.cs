using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ScrollManager : MonoBehaviour, IPointerClickHandler
{
    //QuestMenu
    public GameObject CloseMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToScene();
        }*/
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HideAllMenu();
    }

    public void HideAllMenu()
    {
        // 킉 메뉴 닫기
        Debug.Log("모든 메뉴 닫기");
        CloseMenu.SetActive(false);
        // 모든 자식의 SetActive를 false로 
        for (int i = 0; i < CloseMenu.transform.childCount; i++)
        {
            CloseMenu.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

/*    public void BackToScene()
    {
        Debug.Log("씬이름 " + SceneManager.GetActiveScene().name);
        // 메인 씬이면
        if(SceneManager.GetActiveScene().name == "Main")
        {
            CloseMenu.SetActive(true);
            CloseMenu.transform.Find("ClosePopUp").gameObject.SetActive(true);
        }
        else 
        // 메인 씬이 아니면
        {
            SceneManager.LoadScene("Main");
        }
        
    }*/
}