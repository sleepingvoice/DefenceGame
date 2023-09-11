using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Lobby : MonoBehaviour
{
    public Button StartGame;
	public Button EditMode;

	private GameData _userData = MainGameData.s_gameData;

	private void Awake()
	{
		MainGameData.s_progressMainGame.AddListener(SetActive);
		StartGame.onClick.AddListener(GameStart);
		EditMode.onClick.AddListener(() => MainGameData.s_progressMainGame.SetValue(GameProgress.EditSelect));
	}

	private void SetActive(GameProgress progress)
	{
		this.gameObject.SetActive(progress == GameProgress.Lobby);
	}

	private void GameStart()
	{
		MainGameData.s_progressMainGame.SetValue(GameProgress.GamePlay);

		_userData.NowEnemyNum.SetValue(0);
		_userData.NowMoney.SetValue(500);
		_userData.NowRoundTime.SetValue(1);
	}
}
