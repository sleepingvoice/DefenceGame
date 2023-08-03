using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
	public Button ReturnLobby;

	private void Awake()
	{
		MainGameData.s_progressValue.AddListener(active);
		ReturnLobby.onClick.AddListener(Return);
	}

	private void active(GameProgress Progress)
	{
		this.gameObject.SetActive(Progress == GameProgress.End);
	}

	private void Return()
	{
		MainGameData.s_progressValue.SetValue(GameProgress.Lobby);
	}
}
