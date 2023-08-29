using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using WebSocketSharp;

public class Socket : MonoBehaviour
{
    private static Socket Instance;
    public static Socket ins { get { return Instance;}}

    public float CheckTime;
    public Dictionary<string, Action<string>> SocketEventDic = new Dictionary<string, Action<string>>();
    public GameObject Loding;
    public bool Reset;

    private WebSocket ws;//���� ����
    private bool _checkOpen = false;
    private IEnumerator _openCheck = null;
    private int ConnectInt = 0;

	private void Awake()
	{
        if (Instance == null)
            Instance = this;

    }

	void Start()
    {
        ws = new WebSocket("ws://127.0.0.1:4000/");// 127.0.0.1�� ������ ������ �ּ��̴�. 3333��Ʈ�� �����Ѵٴ� �ǹ��̴�.
        ws.OnMessage += ws_OnMessage; //�������� ����Ƽ ������ �޼����� �� ��� ������ �Լ��� ����Ѵ�.
        ws.OnOpen += ws_OnOpen;//������ ����� ��� ������ �Լ��� ����Ѵ�
        ws.OnClose += ws_OnClose;//������ ���� ��� ������ �Լ��� ����Ѵ�.
        ws.Connect();
    }

	private void Update()
	{
        if (!ws.Ping() && _openCheck == null && ConnectInt < 5)
        {
            _openCheck = CheckConnect();
            StartCoroutine(_openCheck);
            Loding.SetActive(true);
        }

        if (Reset)
        {
            Reset = false;
            ConnectInt = 0;
        }
	}

	private void ws_OnMessage(object sender, MessageEventArgs e)
    {
        var str = e.Data.Split('/');
        SocketEventDic[str[0]].Invoke(str[1]);

        Debug.Log(e.Data);//���� �޼����� ����� �ֿܼ� ����Ѵ�.
    }

    private void ws_OnOpen(object sender, System.EventArgs e)
    {
        ConnectInt = 0;
        _checkOpen = true;
        Debug.Log("open");
        Loding.SetActive(false);
    }

    private void ws_OnClose(object sender, CloseEventArgs e)
    {
        _checkOpen = false;
        Debug.Log("close");
    }

    public void ws_SendMessage(string message)
    {
        if (!_checkOpen)
            return;
        ws.Send(message);
    }

    IEnumerator CheckConnect()
    {
        while (!_checkOpen && ConnectInt < 5)
        {
            yield return new WaitForSeconds(CheckTime); // 1�ʸ��� üũ
            ws.Connect();
            ConnectInt++;
        }

        if (ConnectInt >= 5)
        {
            Debug.Log("������ ������ �����ʽ��ϴ� ������ Ȯ���غ�����");
        }
        _openCheck = null;
    }
}
