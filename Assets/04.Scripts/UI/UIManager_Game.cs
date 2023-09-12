using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Game : MonoBehaviour
{
	public TMP_Text Money;
	public TMP_Text UnityNum;
	public TMP_Text LimitTime;
	public TMP_Text RoundTime;
	public TMP_Text Round;

	public Button ExitBtn;

	private GameData UserData = MainGameData.s_gameData;

	private void Awake()
	{
		MainGameData.s_progressMainGame.AddListener(active);

		UserData.NowMoney.AddListener((value) => Money.text = value.ToString());
		UserData.NowEnemyNum.AddListener((value) => UnityNum.text = value.ToString());
		UserData.NowRoundTime.AddListener((value) => RoundTime.text = value.ToString());
		UserData.NowRound.AddListener((value) => Round.text = value.ToString());

		UserData.NowMoney.SetValue(MainGameData.s_gameData.NowMoney.Value);
		UserData.NowEnemyNum.SetValue(UserData.NowEnemyNum.Value);

		ExitBtn.onClick.AddListener(() => MainGameData.s_progressMainGame.SetValue(GameProgress.Lobby));
	}

	private void active(GameProgress Progress)
	{
		this.gameObject.SetActive(Progress == GameProgress.GamePlay);
	}

}
