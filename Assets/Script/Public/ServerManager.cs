using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class ServerManager : MonoBehaviour
{
    // 서버와 통신하기 위한 웹소켓 변수
    private WebSocket m_WebSocket;

    public UserInfoManager UserInfoManager;

    //private string UserInfoString = "";
    private int UserDataIndex = 0;

    // 서버에서 데이터를 받기 위한 특정 상태 확인용 변수
    /// <summary>
    /// 1 - 로그인, 2 - 유저정보 받기
    /// </summary> 
    private int _state = 1;
    // 프로퍼티에 이벤트 연결
    public int State
    {
        set
        {
            _state = value;
            CheckState();
        }
        get
        {
            return _state;
        }
    }
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        m_WebSocket = new WebSocket("ws://skdiawotjd.iptime.org:11055");
        m_WebSocket.Connect();

        m_WebSocket.OnMessage += (sender, e) =>
        {
            //Debug.Log("서버에서 받은 값 " + e.Data);
            int ParseInt = 0;

            switch (State)
            {
                case 1:
                    // State가 1일 때 서버에 보낸 아이디와 서버에 저장된 아이디가
                    // 존재할 경우 1을 반환, 없을 경우 0을 반환
                    if(int.Parse(string.Format("{0}", e.Data)) == 1)
                    {
                        Debug.Log("1-1. 서버에서 동일한 아이디 확인");
                        // State 변경
                        State = 2;
                    }
                    else
                    {
                        Debug.Log("1-2. 서버에 동일한 아이디가 없음");
                    }
                    break;
                case 2:
                case 3:
                case 4:
                    // State가 2일 때 서버에서 유저의 정보를 User_Info, Risker, Equipment를 순차적으로 보내준다
                    // 처음 State가 2에서 각 값을 받을 때마다 State가 1 씩 증가하여 최종적으로 5가 된다
                    if (!int.TryParse(e.Data, out ParseInt))
                    {
                        Debug.Log("2-1. 받은 유저 정보 : " + e.Data);
                        //
                        // UserInfoManager.UpdatedUserData를 불러야 되는데 UserInfoManager를 불러 저장
                        // public string this[int index]가 아닌 public string[] UpdatedUserData를 부르고 싶음
                        UserInfoManager[UserDataIndex++] = e.Data;
                        //UserInfoManager.UpdatedUserData[UserDataIndex++] = e.Data;
                        //
                        //

                        State++;
                    }
                    break;
            }
            ParseInt = 0;
        };

        CheckState();
    }

    private void CheckState()
    {
        Debug.Log("현재 상태 확인 State = " + State);
        switch(State)
        {
            case 0:
                Debug.Log("서버 통신 대기");
                UserDataIndex = 0;
                break;
            case 1:
                Debug.Log("로그인 시작");
                Login();
                break;
            case 2:
                Debug.Log("UserInfo 데이터 요청");
                RequestUserInfo(0);
                break;
            case 3:
                Debug.Log("Risker 데이터 요청");
                RequestUserInfo(1);
                break;
            case 4:
                Debug.Log("Equipment 데이터 요청");
                RequestUserInfo(2);
                break;
            case 5:
                Debug.Log("데이터를 모두 받아 State를 0으로 변경 후 데이터 저장");
                State = 0;
                UserInfoManager.SetUserData();
                break;
        }
    }

    private void Login()
    {
        Debug.Log("1. 로그인 시작");
        // 서버로 아이디 전송
        m_WebSocket.Send("admin");
    }

    /// <summary>
    /// 0 - 유저정보, 1 - 리스커, 2 - 장비
    /// </summary> 
    public void RequestUserInfo(int UserDataIndex)
    {
        this.UserDataIndex = UserDataIndex;
        //Debug.Log("2. 유저 정보 가져오기 + UserDataIndex = " + this.UserDataIndex.ToString());
        m_WebSocket.Send(this.UserDataIndex.ToString());
    }

    private void ResponseUserInfo()
    {
        Debug.Log("2. 유저 정보 보내기");
    }
}

/*
 * 1. ServerManager와 UserInfoManager를 Login씬으로 이동
 * 2. 로그인 씬에서 서버와 통신하여 정상적으로 로그인이 되면 UserInfoManager에 데이터를 저장
 * 3. Main씬으로 이동
 */
