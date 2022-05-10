using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RequsetUserInfo()
    {
        //Debug.Log("1");
    }
}

/*
 * 1. ServerManager와 UserInfoManager를 Login씬으로 이동
 * 2. 로그인 씬에서 서버와 통신하여 정상적으로 로그인이 되면 UserInfoManager에 데이터를 저장
 * 3. Main씬으로 이동
 */
