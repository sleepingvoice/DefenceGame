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
	private MapData MapInfo = MainGameData.MapInfo;

	private void Awake()
	{
		MainGameData.ProgressValue.AddListener(active);
		MainGameData.Money.AddListener((value) => Money.text = value.ToString());
		MainGameData.EnemyNum.AddListener((value) => UnityNum.text = value.ToString());
		MainGameData.RoundTime.AddListener((value) => RoundTime.text = value.ToString());
		MainGameData.NowRound.AddListener((value) => Round.text = value.ToString());

		MainGameData.Money.SetValue(MainGameData.Money.Value);
		MainGameData.EnemyNum.SetValue(MainGameData.EnemyNum.Value);

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
