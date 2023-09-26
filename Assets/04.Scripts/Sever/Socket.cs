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
	public List<Action> OpenActDic = new List<Action>();
	public GameObject Loding;
	public bool Reset;

	public string ServerIp;

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
		ws = new WebSocket("ws://" + ServerIp + ":4000/");
		Debug.Log(ws.Url);
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

		if (SocketEventDic.ContainsKey(str[0]))
			SocketEventDic[str[0]].Invoke(str[1]);

		Debug.Log(e.Data);//���� �޼����� ����� �ֿܼ� ����Ѵ�.
	}

	private void ws_OnOpen(object sender, System.EventArgs e)
	{
		ConnectInt = 0;
		_checkOpen = true;
		Debug.Log("open");
		Loding.SetActive(false);

		foreach (var act in OpenActDic)
		{
			act.Invoke();
		}
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

	public IEnumerator PostImg(byte[] data, Action<int> actMapID)
	{
		string serverURL = "http://"+ ServerIp +"/imgfile";
		WWWForm form = new WWWForm();
		form.AddBinaryData("image", data, "image.png", "image/png");

		UnityWebRequest webRequest = UnityWebRequest.Post(serverURL, form);

		yield return webRequest.SendWebRequest();
		if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log("��Ʈ��ũ ȯ���� �����Ƽ� ����� �Ҽ� �����ϴ�.");
		}
		else
		{
			actMapID.Invoke(int.Parse(webRequest.downloadHandler.text));
		}

		webRequest.Dispose();
	}

	public IEnumerator GetImg(int ImgId, Action<Texture2D> TexAct)
	{
		string serverURL = "http://localhost:5000/getImage";
		UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(serverURL + "/" + ImgId.ToString());
		yield return webRequest.SendWebRequest();

		if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log("�̹��� �ҷ����� ����: " + webRequest.error);
		}
		else
		{
			Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
			TexAct.Invoke(texture);
		}

		webRequest.Dispose();
	}
}
