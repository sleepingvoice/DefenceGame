using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetGameUi : MonoBehaviour
{
	public TMP_Text Money;
	public TMP_Text UnityNum;
	public TMP_Text LimitTime;
	public TMP_Text RoundTime;
	public TMP_Text Round;

	public Button ShowArea;

	private bool AreaShow = true;
	private MapData MapInfo = MainGameData.s_mapInfo;

	private void Awake()
	{
		MainGameData.s_progressValue.AddListener(active);
		MainGameData.s_money.AddListener((value) => Money.text = value.ToString());
		MainGameData.s_enemyNum.AddListener((value) => UnityNum.text = value.ToString());
		MainGameData.s_roundTime.AddListener((value) => RoundTime.text = value.ToString());
		MainGameData.s_nowRound.AddListener((value) => Round.text = value.ToString());

		MainGameData.s_money.SetValue(MainGameData.s_money.Value);
		MainGameData.s_enemyNum.SetValue(MainGameData.s_enemyNum.Value);

		ShowArea.onClick.AddListener(() => ShowAreaEvent(AreaShow));
	}

	private void active(GameProgress Progress)
	{
		this.gameObject.SetActive(Progress == GameProgress.GamePlay);
	}

	private void ShowAreaEvent(bool Active)
	{
		foreach (var obj in MapInfo.CanBuildObj)
		{
			obj.SetActive(Active);
		}
		foreach (var obj in MapInfo.NotBuildObj)
		{
			obj.SetActive(Active);
		}
		AreaShow = !Active;
	}

}
