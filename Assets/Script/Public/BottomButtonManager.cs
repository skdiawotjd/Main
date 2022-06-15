using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BottomButtonManager : MonoBehaviour
{
    void Start()
    {
        SwitchScene();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            BackToScene();
        }
    }

    private void SwitchScene()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Bar":
                SetMoveSceneButtomButton(0);
                break;
            case "CharacterInfo":
                SetMoveSceneButtomButton(1);
                break;
            case "Collection":
                SetMoveSceneButtomButton(2);
                break;
            case "Deck":
                SetMoveSceneButtomButton(3);
                break;
            case "Incremental Battle":
                SetMoveSceneButtomButton(4);
                break;
            case "Shop":
                SetMoveSceneButtomButton(5);
                break;
            case "Summon":
                SetMoveSceneButtomButton(6);
                break;
        }
    }
    
    private void SetMoveSceneButtomButton(int SceneOrder)
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("UI");
        int Index_One = 0;
        int Index_two = 0;
        int Correct_id = 0;

        while (data_Dialog[Index_One]["Category"].ToString() == "BottomButtonPanel")
        {
            if (int.Parse(string.Format("{0}", data_Dialog[Index_One]["Id"])) == SceneOrder)
            {
                //Debug.Log(data_Dialog[index_One]["Content"].ToString());
                Correct_id = Index_One;
                // ã�� �ݺ����� ������ �ʴ� ������ data_Dialog�� BottomButtonPanel���� MoveButton���� �Ѿ�� �ε����� ����ϱ� ����
            }
            Index_One++;
        }

        Index_two = Index_One;

        for (int i = 0; i < data_Dialog[Correct_id]["Content"].ToString().Length; i++)
        {
            while (data_Dialog[Index_One]["Category"].ToString() == "MoveButton")
            {
                if(data_Dialog[Correct_id]["Content"].ToString()[i] == data_Dialog[Index_One]["Id"].ToString()[0])
                {
                    //Debug.Log(transform.GetChild(i).name + " " + data_Dialog[Index_One]["Content"].ToString());
                    Button SetButton = transform.GetChild(i).GetComponent<Button>();
                    // ��ư Ŭ�� �� �ٸ� ������ �̵��ϱ� ���� �Լ��� �����ʷ� �߰� Closure Problem���� �ӽ� ���� ���
                    int Tem = int.Parse(string.Format("{0}", data_Dialog[Index_One]["Id"]));
                    SetButton.onClick.AddListener(() => { MoveScene(Tem); });
                    // ��ư�� �ؽ�Ʈ ����
                    SetButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = data_Dialog[Index_One]["Content"].ToString();
                    
                    //Debug.Log(data_Dialog[Index_One]["Id"].ToString() + "�� �ؽ�Ʈ�� " + data_Dialog[Index_One]["Content"].ToString());
                    break;
                }
                Index_One++;
            }
            Index_One = Index_two;

        }
    }

    public void BackToScene()
    {
        Debug.Log("���̸� " + SceneManager.GetActiveScene().name);
        // ���� ���̸�
        if (SceneManager.GetActiveScene().name == "Main")
        {
            GameObject CloseMenu = gameObject.transform.Find("CloseMenu").gameObject;

            CloseMenu.SetActive(true);
            CloseMenu.transform.Find("ClosePopUp").gameObject.SetActive(true);
        }
        else
        // ���� ���� �ƴϸ�
        {
            //SceneManager.LoadScene("Main");
            MoveScene(0);
        }

    }

    public void MoveScene(int order)
    {
        switch(order)
        {
            case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
                break;
            case 2:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Bar");
                break;
            case 3:
                UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterInfo");
                break;
            case 4:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Collection");
                break;
            case 5:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Deck");
                break;
            case 6:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
                break;
            case 7:
                UnityEngine.SceneManagement.SceneManager.LoadScene("Summon");
                break;
        }
    }
}
