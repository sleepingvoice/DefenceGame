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
	public bool Reset;

	private WebSocket ws;//소켓 선언
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
		ws = new WebSocket("ws://127.0.0.1:4000/");// 127.0.0.1은 본인의 아이피 주소이다. 3333포트로 연결한다는 의미이다.
		ws.OnMessage += ws_OnMessage; //서버에서 유니티 쪽으로 메세지가 올 경우 실행할 함수를 등록한다.
		ws.OnOpen += ws_OnOpen;//서버가 연결된 경우 실행할 함수를 등록한다
		ws.OnClose += ws_OnClose;//서버가 닫힌 경우 실행할 함수를 등록한다.
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

		Debug.Log(e.Data);//받은 메세지를 디버그 콘솔에 출력한다.
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
			yield return new WaitForSeconds(CheckTime); // 1초마다 체크
			ws.Connect();
			ConnectInt++;
		}

		if (ConnectInt >= 5)
		{
			Debug.Log("서버와 연결이 되지않습니다 서버를 확인해보세요");
		}
		_openCheck = null;
	}

	public IEnumerator PostImg(byte[] data, Action<int> actMapID)
	{
		string serverURL = "http://localhost:5000/imgfile";
		WWWForm form = new WWWForm();
		form.AddBinaryData("image", data, "image.png", "image/png");

		UnityWebRequest webRequest = UnityWebRequest.Post(serverURL, form);

		yield return webRequest.SendWebRequest();
		if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.Log("네트워크 환경이 안좋아서 통신을 할수 없습니다.");
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
			Debug.Log("이미지 불러오기 실패: " + webRequest.error);
		}
		else
		{
			Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
			TexAct.Invoke(texture);
		}

		webRequest.Dispose();
	}
}
