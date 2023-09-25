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
				Debug.LogError("준비된 프로토콜이 부족합니다.");
				return;
			}

			AccountWindow[i].BaseStr = BaseProtocal[i];
		}
	}

	public void CheckSignEmail()
	{
		if (EmailInput.text == null || !Regex.IsMatch(EmailInput.text, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?"))
		{
			Debug.Log("이메일 양식에 맞지않음");
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

	#region 로그인관련 소켓이벤트 처리

	private void AddSocketEvent()
	{
		Socket.ins.SocketEventDic.Add("Check_Login", CheckID);
		Socket.ins.SocketEventDic.Add("Check_Email", CheckEmail);
		Socket.ins.SocketEventDic.Add("Add_ID", AddID);
		Socket.ins.SocketEventDic.Add("Find_ID", FindId);
		Socket.ins.SocketEventDic.Add("Find_Pwd", FindPwd);
	}

	private void CheckID(string callback)
	{
		if (callback == "false")
		{
			Error.SetError("아이디나 비밀번호가 잘못되었습니다.", LoginProgress.main);
		}
		else
		{
			_socketAct = () =>
			{
				Debug.Log("발동");
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
			Error.SetError("이메일이 중복됩니다.", LoginProgress.error);
		}
	}

	private void AddID(string callback)
	{
		if (callback == "Success")
		{
			_socketAct = () =>
			{
				MainGameData.s_progressLogin.SetValue(LoginProgress.main);
			};
		}
	}

	private void FindId(string callback)
	{
		if (callback == "false")
		{
			_socketAct = () =>
			{
				Error.SetError("없는 이메일 주소입니다.", LoginProgress.findID);
			};
		}
		else
		{
			_socketAct = () =>
			{
				Error.SetError("아이디 : " + callback, LoginProgress.main);
			};
		}
	}

	private void FindPwd(string callback)
	{
		if (callback == "false")
		{
			_socketAct = () =>
			{
				Error.SetError("아이디와 이메일이 다릅니다\n 다시한번 확인해주세요", LoginProgress.findPwd);
			};
		}
		else
		{
			_socketAct = () =>
			{
				Error.SetError("비밀번호 : " + callback, LoginProgress.main);
			};
		}
	}

	#endregion
}
