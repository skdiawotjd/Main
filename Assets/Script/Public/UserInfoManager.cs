using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    public UserInfo UserInfo;
    public ServerManager ServerManager;


    private bool _ok = false;

    /// <summary>
    /// 0/0/0 - User_Info/Risker/Equipment가 갱신되었는지 확인
    /// 0이면 갱신되지 않음, 1이면 갱신됨
    /// Index : 0 - User_Info, 1 - Quest , 2 - Risker, 3 - Equipment
    /// </summary>
    private int[] WeatherUpdateData = new int[4] { 0, 0, 0, 0 };

    private string[] _updateduserdata = new string[4];
    // ServerManager의 74Line을 위해 사용
    public string this[int index]
    {
        set
        {
            if (_updateduserdata[index] != value)
            {
                WeatherUpdateData[index] = 1;
                _ok = false;
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

    public bool Ok
    {
        get
        {
            return _ok;
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
    }

    public void SetUserData()
    {
        // 1. 유저정보 / 리스커 / 장비 데이터 중 어느 데이터가 갱신되었는지 확인
        for(int i = 0; i < WeatherUpdateData.Length; i++)
        {
            if (WeatherUpdateData[i] == 1)
            {
                switch(i)
                {
                    case 0:
                        Debug.Log("User_Info가 갱신됨");
                        break;
                    case 1:
                        Debug.Log("Quest가 갱신됨");
                        break;
                    case 2:
                        Debug.Log("Risker가 갱신됨");
                        break;
                    case 3:
                        Debug.Log("Equipment가 갱신됨");
                        break;
                }
            }
        }
    }

    private void SplitString(string SeparatorString, char UntilString, string StringForCount, ref List<string> tem_list)
    {
        int index = 0;
        string tem_str = "";

        Debug.Log(StringForCount);
        for (index = StringForCount.IndexOf(SeparatorString, index); index != -1;)
        {
            // 한번 사용 후 이전 값을 지우기 위해 초기화
            tem_str = "";
            //:"에서 2칸 더 가야 값이므로
            index += 2;
            // "가 나올 때 까지 계속 임시 str에 추가 (:"0000")
            while (StringForCount[index] != UntilString)
            {
                tem_str += StringForCount[index++];
            }
            //Debug.Log(tem_str);
            tem_list.Add(tem_str);

            index = StringForCount.IndexOf(SeparatorString, index);
        }
    }
    public void CreateUserInfo()
    {
        List<string> tem_list = new List<string>();
        // [{"UserCode":"0000","UserName":"skdiawotjd","UserLevel":"1","UserExp":"1","Gold":"1","Diamond":"1"}]
        SplitString(":\"", '\"', UpdatedUserData[0], ref tem_list);

        // UserInfo(string NewUserCode, string NewUserName, int NewUserLevel, double NewUserExp, int NewGold, int NewDiamond,
        //  string NewQuest, string NewRisker, string NewEquipment)
        UserInfo = new UserInfo(tem_list[0], tem_list[1], int.Parse(string.Format("{0}", tem_list[2])), double.Parse(string.Format("{0}", tem_list[3])),
            int.Parse(string.Format("{0}", tem_list[4])), int.Parse(string.Format("{0}", tem_list[5])),
            UpdatedUserData[1], UpdatedUserData[2], UpdatedUserData[3]);

        // 서버에서 받은 데이터를 모두 갱신하였으므로 갱신을 판단하는 변수 초기화
        for(int i = 0; i < WeatherUpdateData.Length; i++)
        {
            WeatherUpdateData[i] = 0;
        }

        _ok = true;
    }
}