using UnityEngine;
using WebSocketSharp;

public class Socket : MonoBehaviour
{
    private static Socket Instance;
    public static Socket ins { get { return Instance;}}
    private WebSocket ws;//���� ����

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
        ws.Connect();//������ �����Ѵ�.
    }

	private void ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);//���� �޼����� ����� �ֿܼ� ����Ѵ�.
    }

    private void ws_OnOpen(object sender, System.EventArgs e)
    {
        Debug.Log("open"); //����� �ֿܼ� "open"�̶�� ��´�.
    }

    private void ws_OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("close"); //����� �ֿܼ� "close"�̶�� ��´�.
    }

    public void ws_SendMessage(string message)
    {
        ws.Send(message);
    }
}
