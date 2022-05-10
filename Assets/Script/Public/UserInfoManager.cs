using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public UserInfo UserInfo;
    public ServerManager ServerManager;

    Equipment BeEuipment;
    Equipment ToBeEuipment;

    private bool _isChange = true;
    private bool IsChange
    {
        get
        {
            return _isChange;
        }
    }

    /*// called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 신이 변경될때마다 호출됨
        Debug.Log("OnSceneLoaded: " + scene.name);
    }*/

    // called third
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //
        // 서버에서 유저 정보를 받아 와 변수에 저장
        ServerManager.RequsetUserInfo();
        UserInfo = new UserInfo("skdiawotjd", 1, 10.0f, 100, 200, 2, "리스커", 3, "장비");
        // 그렇게 했다는 가정 하에
        // 임시
        
        // 정상적으로 데이터를 가져왔다 판단하면 _isChange를 True로 변경
        _isChange = false;

        //
        //임시사용
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        //
        //
    }



    /*public void SetChangeEquipment(int RiskerNumber, int ToBeEuipmentNumber)
    {
        for (int i = 0; i < UserInfo.Riskers.Count; i++)
        {
            if (RiskerNumber == UserInfo.Riskers[i].RiskerNumber)
            {
                for (int k = 0; k < UserInfo.Equipment.Count; k++)
                {
                    if (ToBeEuipmentNumber == UserInfo.Equipment[k].EquipmentNumber)
                    {
                        for (int m = 0; m < UserInfo.Riskers[i].ActiveEquipment.Count; m++)
                        {
                            Debug.Log(UserInfo.Equipment[k].StatOrder + " == " + UserInfo.Riskers[i].ActiveEquipment[m].StatOrder);
                            if (UserInfo.Equipment[k].StatOrder == UserInfo.Riskers[i].ActiveEquipment[m].StatOrder)
                            {
                                *//*Equipment TemEquipment = UserInfo.Equipment[k];
                                UserInfo.Equipment[k] = UserInfo.Riskers[i].ActiveEquipment[m];
                                UserInfo.Riskers[i].ActiveEquipment[m] = TemEquipment;*//*

                                BeEuipment = UserInfo.Riskers[i].ActiveEquipment[m];
                                ToBeEuipment = UserInfo.Equipment[k];
                                break;
                            }

                        }
                        break;
                    }
                }
                break;
            }
        }
    }
    public void ApplyEquipment()
    {
        Equipment TemEquipment = ToBeEuipment;
        ToBeEuipment = BeEuipment;
        BeEuipment = TemEquipment;
    }*/
}

/*
UserInfoManager는 처음 시작 시 서버에서 유저의 정보를 받아와 저장한다 - 이는 각 씬에서 UI로 유저의 정보를 보여줄 때 사용
                                                                    실제 이 정보를 사용할 때에는 서버에 다시 요청해서 사용(보안을 위해)

 
*/