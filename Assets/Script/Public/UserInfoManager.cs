using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public UserInfo UserInfo;
    public ServerManager ServerManager;


    private bool Ok = false;

    /// <summary>
    /// 0/0/0 - User_Info/Risker/Equipment가 갱신되었는지 확인
    /// 0이면 갱신되지 않음, 1이면 갱신됨
    /// </summary>
    private string Updated = "";
    private int[] WeatherUpdateData = new int[3] { 0, 0, 0 };
    /// <summary>
    /// 1 - User_Info, 2 - Risker, 3 Equipment
    /// </summary> 
    /*private int[] WeatherUpdateData
    {
        set
        {
            _weatherupdatedata = value;
        }
        get
        {
            return _weatherupdatedata;
        }
    }*/

    private string[] _updateduserdata = new string[3];
    // ServerManager의 74Line을 위해 사용
    public string this[int index]
    {
        set
        {
            if (_updateduserdata[index] != value)
            {
                WeatherUpdateData[index] = 1;
                Ok = false;
            }
            _updateduserdata[index] = value;
        }
        get
        {
            return _updateduserdata[index];
        }
    }
    public string[] UpdatedUserData
    {
        set
        {
            _updateduserdata = value;
        }
        get
        {
            return _updateduserdata;
        }
    }

    /*private bool _isChange = true;
    private bool IsChange
    {
        get
        {
            return _isChange;
        }
    }*/

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
    }

    public void SetUserData()
    {
        
        // 1. 유저정보 / 리스커 / 장비 데이터 중 어느 데이터가 갱신되었는지 확인
        for(int i = 0; i < WeatherUpdateData.Length; i++)
        {
            if (WeatherUpdateData[i] == 1)
            {
                Updated += "1";
            }
            else
            {
                Updated += "0";
            }
        }

        // 2. 갱신된 부분에 따라 특정 값 갱신
        switch(Updated)
        {
            case "000":
                Debug.Log("갱신된 데이터가 없음");
                break;
            case "001":
                Debug.Log("User_Info가 갱신됨");
                break;
            case "010":
                Debug.Log("Risker가 갱신됨");
                break;
            case "100":
                Debug.Log("Equipment가 갱신됨");
                break;
            case "011":
                Debug.Log("User_Info, Risker가 갱신됨");
                break;
            case "101":
                Debug.Log("User_Info, Equipment가 갱신됨");
                break;
            case "110":
                Debug.Log("Risker, Equipment가 갱신됨");
                break;
            case "111":
                Debug.Log("User_Info, Risker, Equipment가 갱신됨");
                CreateUserInfo();
                break;
        }
    }

    // User_Info, Risker, Equipment가 각각 갱신되는 함수 필요


    private void CreateUserInfo()
    {
        int index = 0;
        string tem_str = "";
        List<string> tem_list = new List<string>();

        Debug.Log(UpdatedUserData[0]);
        for (index = UpdatedUserData[0].IndexOf(":\"", index); index != -1;)
        {
            // 한번 사용 후 이전 값을 지우기 위해 초기화
            tem_str = "";
            //:"에서 2칸 더 가야 값이므로
            index += 2;
            // "가 나올 때 까지 계속 임시 str에 추가 (:"0000")
            while (UpdatedUserData[0][index] != '\"')
            {
                tem_str += UpdatedUserData[0][index++];
            }
            //Debug.Log(tem_str);
            tem_list.Add(tem_str);

            index = UpdatedUserData[0].IndexOf(":\"", index);
        }

        UserInfo = new UserInfo(tem_list[0], tem_list[1], int.Parse(string.Format("{0}", tem_list[2])), double.Parse(string.Format("{0}", tem_list[3])),
            int.Parse(string.Format("{0}", tem_list[4])), int.Parse(string.Format("{0}", tem_list[5])),
            UpdatedUserData[1], UpdatedUserData[2]);

        /*UserInfo = new UserInfo("skdiawotjd", 1, 10.0f, 100, 200, 
            2, "리스커", 3, "장비");*/
        // public UserInfo(string NewUserName, int NewUserLevel, double NewUserExp, int NewGold, int NewDiamond,
        // int NewCountRisker, string NewRisker, int NewCountEquipment, string NewEquipment)

        // 서버에서 받은 데이터를 모두 갱신하였으므로 갱신을 판단하는 변수 초기화
        Updated = "";
        for(int i = 0; i < WeatherUpdateData.Length; i++)
        {
            WeatherUpdateData[i] = 0;
        }

        Ok = true;
    }

    public bool IsReady()
    {
        return Ok;
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
                                Equipment TemEquipment = UserInfo.Equipment[k];
                                UserInfo.Equipment[k] = UserInfo.Riskers[i].ActiveEquipment[m];
                                UserInfo.Riskers[i].ActiveEquipment[m] = TemEquipment;

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
    }*/
    /*public void ApplyEquipment()
    {
        Equipment TemEquipment = ToBeEuipment;
        ToBeEuipment = BeEuipment;
        BeEuipment = TemEquipment;
    }*/
}

/*
UserInfoManager는 처음 시작 시 서버에서 유저의 정보를 받아와 저장한다 - 이는 각 씬에서 UI로 유저의 정보를 보여줄 때 사용

 
*/