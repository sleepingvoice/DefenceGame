using UnityEngine;
using UnityEngine.UI;

public class UIManager_Lobby : MonoBehaviour
{
	public Button StartGame;
	public Button EditMode;

	private GameData _userData = MainGameData.s_gameData;
	private ServerData _serverData = MainGameData.s_serverData;

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
		GetBaseData();
		GameManager.ins.GameStart();
	}

	private void GetBaseData()
	{
		foreach (var info in _serverData.MapinfoSever.List)
		{
			if (info.userId != 0)
				continue;
			_serverData.NowMap.SetValue(info);
		}
	}
}
