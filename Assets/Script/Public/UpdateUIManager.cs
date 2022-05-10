using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.SceneManagement;

public class UpdateUIManager : MonoBehaviour
{
    private UserInfoManager UserInfomation;

    public TextMeshProUGUI TextLevel;
    public TextMeshProUGUI TextName;
    public TextMeshProUGUI TextGold;
    public TextMeshProUGUI TextDiamond;


    void Start()
    {
        UserInfomation = GameObject.Find("UserInfoManager").GetComponent<UserInfoManager>();
        UpdateUserInfomation();
        //StartCoroutine("NullCheckUpdateUserInfo");
    }

    /*IEnumerator NullCheckUpdateUserInfo()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.05f);

        if (UserInfomation == null)
        {
            UserInfomation = GameObject.Find("UserInfo").GetComponent<UserInfoManager>();
        }

        while (!UserInfomation.IsChange)
        {
            yield return wait;
        }
        UpdateUserInfomation();
        //UpdateUserInfo();
    }*/


    private void UpdateUserInfo()
    {
        /*switch(SceneManager.GetActiveScene().name)
        {
            case "Main" :
                UpdateUserInfoCaseOne();
                break;
            case "CharacterInfo":
                UpdateUserInfoCaseOne();
                break;
            case "Deck":
                UpdateUserInfoCaseOne();
                break;
            case "Bar":
                UpdateUserInfoCaseOne();
                break;
            case "Shop":
                UpdateUserInfoCaseOne();
                break;
            case "Battle":
                UpdateUserInfoCaseTwo();
                break;
        }*/

    }

    public void UpdateUserInfomation()
    {
        TextName.text = UserInfomation.UserInfo.UserName;
        TextLevel.text = UserInfomation.UserInfo.UserLevel.ToString();
        TextGold.text = UserInfomation.UserInfo.Gold.ToString();
        TextDiamond.text = UserInfomation.UserInfo.Diamond.ToString();
        
    }
    /*private void UpdateUserInfoCaseTwo()
    {
        TextLevel.text = UserInfomation.UserInfo.Level.ToString();
        TextGold.text = UserInfomation.UserInfo.Gold.ToString();
        TextDiamond.text = UserInfomation.UserInfo.Diamond.ToString();
    }*/
}
