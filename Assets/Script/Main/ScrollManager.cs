using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject CloseMenu;

    void Start()
    {

    }

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
        // �� �޴� �ݱ�
        Debug.Log("��� �޴� �ݱ�");
        // ��� �ڽ��� SetActive�� false�� 
        for (int i = 0; i < CloseMenu.transform.childCount; i++)
        {
            CloseMenu.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

/*    public void BackToScene()
    {
        Debug.Log("���̸� " + SceneManager.GetActiveScene().name);
        // ���� ���̸�
        if(SceneManager.GetActiveScene().name == "Main")
        {
            CloseMenu.SetActive(true);
            CloseMenu.transform.Find("ClosePopUp").gameObject.SetActive(true);
        }
        else 
        // ���� ���� �ƴϸ�
        {
            SceneManager.LoadScene("Main");
        }
        
    }*/
}