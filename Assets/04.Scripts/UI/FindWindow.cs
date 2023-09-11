using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindWindow : MonoBehaviour
{
	public LoginProgress TargetProgrees;
	public Button ID_FindBtn;
	public Button Pwd_FindBtn;
	public Button ExitBtn;

	public void Awake()
	{
		MainGameData.s_progressLogin.AddListener((value) => this.gameObject.SetActive(value == TargetProgrees));
		ID_FindBtn.onClick.AddListener(() => MainGameData.s_progressLogin.SetValue(LoginProgress.findID));
		Pwd_FindBtn.onClick.AddListener(() => MainGameData.s_progressLogin.SetValue(LoginProgress.findPwd));
		ExitBtn.onClick.AddListener(() => MainGameData.s_progressLogin.SetValue(LoginProgress.main));
	}
}
