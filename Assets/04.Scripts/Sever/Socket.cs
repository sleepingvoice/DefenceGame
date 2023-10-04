using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

public class Socket : MonoBehaviour
{
	private static Socket Instance;
	public static Socket ins
	{
		get
		{
			return Instance;
		}
	}

	public float CheckTime;
	public Dictionary<string, Action<string>> SocketEventDic = new Dictionary<string, Action<string>>();
	public GameObject Loding;

	public string ServerIp;

	private WebSocket ws;//���� ����
	private bool _checkOpen = false;
	private IEnumerator _openCheck = null;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

	}

	void Start()
	{
		ws = new WebSocket("ws://" + ServerIp + ":4000/");
		Debug.Log(ws.Url);
		ws.OnMessage += ws_OnMessage; //�������� ����Ƽ ������ �޼����� �� ��� ������ �Լ��� ����Ѵ�.
		ws.OnOpen += ws_OnOpen;//������ ����� ��� ������ �Լ��� ����Ѵ�
		ws.OnClose += ws_OnClose;//������ ���� ��� ������ �Լ��� ����Ѵ�.
		ws.Connect();

		_openCheck = CheckConnect();
	}

	private void ws_OnMessage(object sender, MessageEventArgs e)
	{
		var str = e.Data.Split('/');

		if (SocketEventDic.ContainsKey(str[0]))
			SocketEventDic[str[0]].Invoke(str[1]);

		Debug.Log(e.Data);//���� �޼����� ����� �ֿܼ� ����Ѵ�.
	}

	private void ws_OnOpen(object sender, System.EventArgs e)
	{
		_checkOpen = true;
		Debug.Log("open");

		StopCoroutine(_openCheck);
		Loding.SetActive(false);

	}

	private void ws_OnClose(object sender, CloseEventArgs e)
	{
		_checkOpen = false;
		Debug.Log("close");

		StartCoroutine(_openCheck);
		Loding.SetActive(true);
	}

	public void ws_SendMessage(string message)
	{
		if (!_checkOpen)
			return;
		ws.Send(message);
	}

	IEnumerator CheckConnect()
	{
		int ConnectInt = 0;
		while (ConnectInt < 5)
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
