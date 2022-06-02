using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class ServerManager : MonoBehaviour
{
    // ������ ����ϱ� ���� ������ ����
    private WebSocket m_WebSocket;

    // �������� �����͸� �ޱ� ���� Ư�� ���� Ȯ�ο� ����
    // 1 - �α���, 2 - �������� �ޱ�
    int _state = 1;
    // ������Ƽ�� �̺�Ʈ ����
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
            //Debug.Log("�������� ���� �� " + e.Data);
            int ParseInt = 0;

            switch (State)
            {
                case 1:
                    // State�� 1�� �� ������ ���� ���̵�� ������ ����� ���̵�
                    // ������ ��� 1�� ��ȯ, ���� ��� 0�� ��ȯ
                    if(int.Parse(string.Format("{0}", e.Data)) == 1)
                    {
                        Debug.Log("1-1. �������� ������ ���̵� Ȯ��");
                        // State ����
                        State = 2;
                    }
                    else
                    {
                        Debug.Log("1-2. ������ ������ ���̵� ����");
                    }
                    break;
                case 2:
                    // State�� 2�� �� �������� ������ ������ ĳ���� ������ŭ �����ش�
                    // ���� �� ������ ���������� 3�� ������
                    if(!int.TryParse(e.Data, out ParseInt))
                    {
                        Debug.Log("2-1. ���� ���� ���� : " + e.Data);
                    }
                    else
                    {
                        if(ParseInt == 3)
                        {
                            Debug.Log("�������� ���� ���� �� ����");
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
        Debug.Log("���� ���� Ȯ�� State = " + State);
        switch(State)
        {
            case 0:
                Debug.Log("���� ��� ���");
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
        Debug.Log("1. �α��� ����");
        // ������ ���̵� ����
        m_WebSocket.Send("admin");
    }

    private void RequsetUserInfo()
    {
        Debug.Log("2. ���� ���� ��������");
        m_WebSocket.Send("0");
    }

    private void ResponseUserInfo()
    {
        Debug.Log("2. ���� ���� ������");
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
 * 1. ServerManager�� UserInfoManager�� Login������ �̵�
 * 2. �α��� ������ ������ ����Ͽ� ���������� �α����� �Ǹ� UserInfoManager�� �����͸� ����
 * 3. Main������ �̵�
 */
