using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour//, IPointerClickHandler
{
    // 터치할 오브젝트가 있는 레이어 위치
    private int LayerMask = 1 << 9;
    // 레이어 최대 길이
    private float MaxDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 테스트용
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 TouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(TouchPos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(TouchPos, Vector2.zero, MaxDistance, LayerMask);
            if (hit)
            {
                //tem.text = hit.transform.gameObject.name;
                Debug.Log("touch obj name : " + hit.transform.gameObject.name);
            }
        }
        
    }

/*    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData);
        //Click Event
        Vector2 TouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask);
        RaycastHit2D hit = Physics2D.Raycast(TouchPos, Vector2.zero, MaxDistance, LayerMask);
        if (hit)
        {
            tem.text = hit.transform.gameObject.name;
            MoveScene(hit.transform.gameObject.name);
        }
        
    }*/

    public void MoveScene(string ClickedObjectName)
    {
        switch (ClickedObjectName)
        {
            case "1Battle":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
                break;
            case "2CharacterInfo":
                UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterInfo");
                break;
            case "3Shop":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
                break;
            case "4Summon":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Summon");
                break;
            case "5Collection":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Collection");
                break;
            case "6Agit":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Agit");
                break;
            case "7Bar":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Bar");
                break;
        }
    }
}
