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
 * 1. ServerManager�� UserInfoManager�� Login������ �̵�
 * 2. �α��� ������ ������ ����Ͽ� ���������� �α����� �Ǹ� UserInfoManager�� �����͸� ����
 * 3. Main������ �̵�
 */
