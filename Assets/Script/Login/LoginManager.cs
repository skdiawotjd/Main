using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public Button StartButton;

    private UserInfoManager UserInfoManager;

    void Start()
    {
        UserInfoManager = GameObject.Find("UserInfoManager").GetComponent<UserInfoManager>();

        StartCoroutine("StartGame");
    }

    public void MoveMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    IEnumerator StartGame()
    {
        while(!UserInfoManager.Ok)
        {
            yield return new WaitForSeconds(0.01f);
        }
        StartButton.gameObject.SetActive(true);
    }
}
