using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Lobby : MonoBehaviour
{
    public Button StartGame;
	public Button EditMode;

	private Action _socketAct = null;
	private bool _first = false;

	private void Awake()
	{
		MainGameData.s_progressValue.AddListener(SetActive);
		StartGame.onClick.AddListener(GameStart);
		EditMode.onClick.AddListener(() => MainGameData.s_progressValue.SetValue(GameProgress.EditSelect));

		Socket.ins.SocketEventDic.Add("Get_MapList", MapListGet);


	}

	private void Update()
	{
		if (_socketAct != null)
			_socketAct.Invoke();
	}

	private void SetActive(GameProgress progress)
	{
		this.gameObject.SetActive(progress == GameProgress.Lobby);

		if (!_first)
		{
			Socket.ins.ws_SendMessage("Get_MapList");
		}
	}

	private void GameStart()
	{
		MainGameData.s_progressValue.SetValue(GameProgress.GamePlay);

		MainGameData.s_enemyNum.SetValue(0);
		MainGameData.s_money.SetValue(500);
		MainGameData.s_nowRound.SetValue(1);
	}

	private void MapListGet(string callback)
	{
		var chagneCallback = "{\"List\":" + callback + "}";
		_socketAct = () =>
		{
			MainGameData.s_mapData.GetMapInfo = JsonUtility.FromJson<MapList>(chagneCallback);

			_socketAct = null;
		};
	}
}
