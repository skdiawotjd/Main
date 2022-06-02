using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class ServerManager : MonoBehaviour
{
    // 서버와 통신하기 위한 웹소켓 변수
    private WebSocket m_WebSocket;

    // 서버에서 데이터를 받기 위한 특정 상태 확인용 변수
    // 1 - 로그인, 2 - 유저정보 받기
    int _state = 1;
    // 프로퍼티에 이벤트 연결
    int State
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
                    // State가 2일 때 서버에서 유저의 정보를 캐릭터 개수만큼 보내준다
                    // 전부 다 보내면 마지막으로 3을 보낸다
                    if(!int.TryParse(e.Data, out ParseInt))
                    {
                        Debug.Log("2-1. 받은 유저 정보 : " + e.Data);
                    }
                    else
                    {
                        if(ParseInt == 3)
                        {
                            Debug.Log("서버에서 유저 정보 다 받음");
                            State = 0;
                        }
                    }
                    break;
            }
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
                break;
            case 1:
                Login();
                break;
            case 2:
                RequsetUserInfo();
                break;
        }
    }

    private void Login()
    {
        Debug.Log("1. 로그인 시작");
        // 서버로 아이디 전송
        m_WebSocket.Send("admin");
    }

    private void RequsetUserInfo()
    {
        Debug.Log("2. 유저 정보 가져오기");
        m_WebSocket.Send("0");
    }

    private void ResponseUserInfo()
    {
        Debug.Log("2. 유저 정보 보내기");
    }

    public void GetUserInfo()
    {
        //Debug.Log("1");
    }
    public void SetUserInfo()
    {
        //State = 4;
    }
}

/*
 * 1. ServerManager와 UserInfoManager를 Login씬으로 이동
 * 2. 로그인 씬에서 서버와 통신하여 정상적으로 로그인이 되면 UserInfoManager에 데이터를 저장
 * 3. Main씬으로 이동
 */
