using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLobbyUi : MonoBehaviour
{
    public Button StartGame;

	private void Awake()
	{
		MainGameData.ProgressValue.AddListener(active);
		StartGame.onClick.AddListener(GameStart);
	}

	private void active(GameProgress Progress)
	{
		this.gameObject.SetActive(Progress == GameProgress.Lobby);
	}

	private void GameStart()
	{
		MainGameData.ProgressValue.SetValue(GameProgress.GamePlay);
		MainGameData.NowRound.SetValue(1);
	}
}
