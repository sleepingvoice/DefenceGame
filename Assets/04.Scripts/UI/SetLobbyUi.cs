using UnityEngine;
using UnityEngine.UI;

public class SetLobbyUi : MonoBehaviour
{
    public Button StartGame;
	public Button EditMode;

	private void Awake()
	{
		MainGameData.s_progressValue.AddListener(active);
		StartGame.onClick.AddListener(GameStart);
		EditMode.onClick.AddListener(EditStart);
	}

	private void active(GameProgress Progress)
	{
		this.gameObject.SetActive(Progress == GameProgress.Lobby);
	}

	private void GameStart()
	{
		MainGameData.s_progressValue.SetValue(GameProgress.GamePlay);
		Init();
	}

	private void EditStart()
	{
		MainGameData.s_progressValue.SetValue(GameProgress.Edit);
	}


	private void Init()
	{
		MainGameData.s_enemyNum.SetValue(0);
		MainGameData.s_money.SetValue(500);
		MainGameData.s_nowRound.SetValue(1);
	}
}
