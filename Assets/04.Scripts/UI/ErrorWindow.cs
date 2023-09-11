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
		MainGameData.s_progressLogin.AddListener((value) => this.gameObject.SetActive(value == LoginProgress.error));
		ExitBtn.onClick.AddListener(() => MainGameData.s_progressLogin.SetValue(BeforeProgress));
	}

	public void SetError(string str,LoginProgress before)
	{
		BeforeProgress = before;

		str = str.Replace("\\n", "\n");
		Detail.text = str;

		this.gameObject.SetActive(true);
		MainGameData.s_progressLogin.SetValue(LoginProgress.error);
	}
}
