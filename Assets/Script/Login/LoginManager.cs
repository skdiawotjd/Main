using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public Button StartButton;

    private UserInfoManager UserInfoManager;

    // Start is called before the first frame update
    void Start()
    {
        UserInfoManager = GameObject.Find("UserInfoManager").GetComponent<UserInfoManager>();

        StartCoroutine("StartGame");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    IEnumerator StartGame()
    {
        while(!UserInfoManager.IsReady())
        {
            yield return new WaitForSeconds(0.01f);
        }
        StartButton.gameObject.SetActive(true);
    }
}
