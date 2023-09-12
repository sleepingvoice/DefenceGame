using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UIManager_Login : MonoBehaviour
{
	[Header("List")]
	public List<AccountUI> AccountWindow;
	public List<string> BaseProtocal;

	[Header("OtherWindow")]
	public AccountUI SignUI;
	public Button RegistBtn;
	public Button FindBtn;

	[Header("EmailCheck")]
	public TMP_InputField EmailInput;
	public Button EmailBtn;
	public Button SignBtn;

	public ErrorWindow Error;

	private Action _socketAct = null;

	public void Awake()
	{
		SetBase();
		EmailBtn.onClick.AddListener(CheckSignEmail);
		RegistBtn.onClick.AddListener(() =>
		{
			MainGameData.s_progressLogin.SetValue(LoginProgress.signup);
			SignBtn.interactable = false;
		});
		FindBtn.onClick.AddListener(() => MainGameData.s_progressLogin.SetValue(LoginProgress.find));

		AddSocketEvent();
	}

	private void SetBase()
	{
		for (int i = 0; i < AccountWindow.Count; i++)
		{
			if (BaseProtocal.Count <= i)
			{
				Debug.LogError("�غ�� ���������� �����մϴ�.");
				return;
			}

			AccountWindow[i].BaseStr = BaseProtocal[i];
		}
	}

	private void Start()
	{
		MainGameData.s_progressLogin.SetValue(LoginProgress.main);
	}

	public void CheckSignEmail()
	{
		if (EmailInput.text == null || !Regex.IsMatch(EmailInput.text, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?"))
		{
			Debug.Log("�̸��� ��Ŀ� ��������");
			return;
		}

		string massage = "Check_Email/" + EmailInput.text;

		Socket.ins.ws_SendMessage(massage);
	}

	private void Update()
	{
		if (_socketAct != null)
		{
			_socketAct.Invoke();
			_socketAct = null;
		}
	}

	#region �α��ΰ��� �����̺�Ʈ ó��

	private void AddSocketEvent()
	{
		Socket.ins.SocketEventDic.Add("Check_Login", CheckID);
		Socket.ins.SocketEventDic.Add("Check_Email", CheckEmail);
		Socket.ins.SocketEventDic.Add("Find_ID", FindId);
		Socket.ins.SocketEventDic.Add("Find_Pwd", FindPwd);
	}

	private void CheckID(string callback)
	{
		if (callback == "false")
		{
			Error.SetError("���̵� ��й�ȣ�� �߸��Ǿ����ϴ�.", LoginProgress.main);
		}
		else
		{
			_socketAct = () =>
			{
				var str = callback.Split(':');
				MainGameData.s_serverData.UserName = str[0];
				MainGameData.s_serverData.UserId = int.Parse(str[1]);
				MainGameData.s_progressLogin.SetValue(LoginProgress.finish);
				MainGameData.s_progressMainGame.SetValue(GameProgress.Lobby);
			};
		}
	}

	private void CheckEmail(string callback)
	{
		if (callback == "true")
		{
			_socketAct = () =>
			{
				SignBtn.interactable = true;
			};
		}
		else
		{
			SignUI.CheckNoDelete = true;
			Error.SetError("�̸����� �ߺ��˴ϴ�.", LoginProgress.error);
		}
	}

	private void FindId(string callback)
	{
		if (callback == "false")
		{
			_socketAct = () =>
			{
				Error.SetError("���� �̸��� �ּ��Դϴ�.", LoginProgress.findID);
			};
		}
		else
		{
			_socketAct = () =>
			{
				Error.SetError("���̵� : " + callback, LoginProgress.main);
			};
		}
	}

	private void FindPwd(string callback)
	{
		if (callback == "false")
		{
			_socketAct = () =>
			{
				Error.SetError("���̵�� �̸����� �ٸ��ϴ�\n �ٽ��ѹ� Ȯ�����ּ���", LoginProgress.findPwd);
			};
		}
		else
		{
			_socketAct = () =>
			{
				Error.SetError("��й�ȣ : " + callback, LoginProgress.main);
			};
		}
	}

	#endregion
}
