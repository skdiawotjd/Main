using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class ServerManager : MonoBehaviour
{
    // ������ ����ϱ� ���� ������ ����
    private WebSocket m_WebSocket;

    public UserInfoManager UserInfoManager;

    //private string UserInfoString = "";
    private int UserDataIndex = 0;

    // �������� �����͸� �ޱ� ���� Ư�� ���� Ȯ�ο� ����
    /// <summary>
    /// 1 - �α���, 2 - �������� �ޱ�
    /// </summary> 
    private int _state = 1;
    // ������Ƽ�� �̺�Ʈ ����
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
                case 3:
                case 4:
                    // State�� 2�� �� �������� ������ ������ User_Info, Risker, Equipment�� ���������� �����ش�
                    // ó�� State�� 2���� �� ���� ���� ������ State�� 1 �� �����Ͽ� ���������� 5�� �ȴ�
                    if (!int.TryParse(e.Data, out ParseInt))
                    {
                        Debug.Log("2-1. ���� ���� ���� : " + e.Data);
                        //
                        // UserInfoManager.UpdatedUserData�� �ҷ��� �Ǵµ� UserInfoManager�� �ҷ� ����
                        // public string this[int index]�� �ƴ� public string[] UpdatedUserData�� �θ��� ����
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
        Debug.Log("���� ���� Ȯ�� State = " + State);
        switch(State)
        {
            case 0:
                Debug.Log("���� ��� ���");
                UserDataIndex = 0;
                break;
            case 1:
                Debug.Log("�α��� ����");
                Login();
                break;
            case 2:
                Debug.Log("UserInfo ������ ��û");
                RequestUserInfo(0);
                break;
            case 3:
                Debug.Log("Risker ������ ��û");
                RequestUserInfo(1);
                break;
            case 4:
                Debug.Log("Equipment ������ ��û");
                RequestUserInfo(2);
                break;
            case 5:
                Debug.Log("�����͸� ��� �޾� State�� 0���� ���� �� ������ ����");
                State = 0;
                UserInfoManager.SetUserData();
                break;
        }
    }

    private void Login()
    {
        Debug.Log("1. �α��� ����");
        // ������ ���̵� ����
        m_WebSocket.Send("admin");
    }

    /// <summary>
    /// 0 - ��������, 1 - ����Ŀ, 2 - ���
    /// </summary> 
    public void RequestUserInfo(int UserDataIndex)
    {
        this.UserDataIndex = UserDataIndex;
        //Debug.Log("2. ���� ���� �������� + UserDataIndex = " + this.UserDataIndex.ToString());
        m_WebSocket.Send(this.UserDataIndex.ToString());
    }

    private void ResponseUserInfo()
    {
        Debug.Log("2. ���� ���� ������");
    }
}

/*
 * 1. ServerManager�� UserInfoManager�� Login������ �̵�
 * 2. �α��� ������ ������ ����Ͽ� ���������� �α����� �Ǹ� UserInfoManager�� �����͸� ����
 * 3. Main������ �̵�
 */
