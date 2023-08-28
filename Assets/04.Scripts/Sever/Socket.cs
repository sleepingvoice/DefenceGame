using UnityEngine;
using WebSocketSharp;

public class Socket : MonoBehaviour
{
    private static Socket Instance;
    public static Socket ins { get { return Instance;}}
    private WebSocket ws;//소켓 선언

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
        ws.Connect();//서버에 연결한다.
    }

	private void ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);//받은 메세지를 디버그 콘솔에 출력한다.
    }

    private void ws_OnOpen(object sender, System.EventArgs e)
    {
        Debug.Log("open"); //디버그 콘솔에 "open"이라고 찍는다.
    }

    private void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("close"); //디버그 콘솔에 "close"이라고 찍는다.
    }

    public void ws_SendMessage(string message)
    {
        ws.Send(message);
    }
}
