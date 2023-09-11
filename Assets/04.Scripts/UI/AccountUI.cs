using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountUI : MonoBehaviour
{
	public LoginProgress TargetProgrees;
	[Space(10)]
	public List<TMP_InputField> InputList = new List<TMP_InputField>();
	public List<string> InputErrorLog = new List<string>();
	[Space(10)]
	public Button EnterBtn;
	public Button ExitBtn;

	public ErrorWindow Error;

	[HideInInspector]public bool CheckNoDelete = false;
	[HideInInspector] public string BaseStr = "";

	private void Awake()
	{
		MainGameData.s_progressLogin.AddListener((value) => this.gameObject.SetActive(value == TargetProgrees));

		if (ExitBtn != null)
			ExitBtn.onClick.AddListener(() => 
			{
				this.gameObject.SetActive(false);
				MainGameData.s_progressLogin.SetValue(LoginProgress.main);
			});

		EnterBtn.onClick.AddListener(EnterEvent);
	}

	private void OnEnable()
	{
		if (CheckNoDelete)
		{
			CheckNoDelete = true;
			return;
		}

		foreach (var input in InputList)
		{
			input.text = "";
		}
	}

	private void EnterEvent()
	{
		if (ErrorCheck())
			Socket.ins.ws_SendMessage(AddResult());
	}

	private bool ErrorCheck()
	{
		for (int i = 0; i < InputList.Count; i++)
		{
			if (InputList[i].text == "")
			{
				if (InputErrorLog.Count > i)
					Error.SetError(InputErrorLog[i], TargetProgrees);
				else
					Debug.LogError(this.gameObject.name + " 에 입력이 없었지만 에러로그가 없습니다");

				return false;
			}
		}
		return true;
	}

	private string AddResult()
	{
		string result = BaseStr;
		foreach (var input in InputList)
		{
			result += "/" + input.text;
		}
		return result;
	}
}
