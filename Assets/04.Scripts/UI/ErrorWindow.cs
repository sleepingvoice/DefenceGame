using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorWindow : MonoBehaviour
{
	[HideInInspector] public LoginProgress BeforeProgress;
	public TMP_Text Detail;
	public Button ExitBtn;

	private void Awake()
	{
		MainGameData.s_loginProgress.AddListener((value) => this.gameObject.SetActive(value == LoginProgress.error));
		ExitBtn.onClick.AddListener(() => MainGameData.s_loginProgress.SetValue(BeforeProgress));
	}

	public void SetError(string str,LoginProgress before)
	{
		BeforeProgress = before;
		Detail.text = str;
		this.gameObject.SetActive(true);
		MainGameData.s_loginProgress.SetValue(LoginProgress.error);
	}
}
