using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;



public class LoginManager : MonoBehaviour
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
		RegistBtn.onClick.AddListener(() => { 
			MainGameData.s_loginProgress.SetValue(LoginProgress.signup);
			SignBtn.interactable = false;
		});
		FindBtn.onClick.AddListener(() => MainGameData.s_loginProgress.SetValue(LoginProgress.findID));

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

	private void Start()
	{
		MainGameData.s_loginProgress.SetValue(LoginProgress.main);
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
	}

	private void CheckID(string callback)
	{
		if (callback == "true")
		{
			_socketAct = () =>
			{
				MainGameData.s_loginProgress.SetValue(LoginProgress.finish);
				MainGameData.s_progressValue.SetValue(GameProgress.Lobby);
			};
		}
		else
		{
			Error.SetError("아이디나 비밀번호가 잘못되었습니다.", LoginProgress.main);
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

	#endregion
}
