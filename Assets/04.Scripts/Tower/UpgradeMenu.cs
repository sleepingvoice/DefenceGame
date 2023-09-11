using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
	public ChessRank MenuRank;
	[HideInInspector]public int Price;

	public Button ClickBtn;
	public TMP_Text PriceValue;
	private bool _checkGame;

	private void Awake()
	{
		MainGameData.s_progressMainGame.AddListener((value) =>
		{
			_checkGame = value == GameProgress.GamePlay ? true : false;
		});

		MainGameData.GameTouchMap.InsertDic((value) =>
		{
			if (value == null || !_checkGame)
				return;

			this.gameObject.SetActive(false);

			var list = MainGameData.s_clientData.NextRank.ReturnNextRank(value.NowRank);

			if (list == null)
			{
				MainGameData.GameTouchMap.SetValue(null);
				return;
			}

			foreach (var info in list)
			{
				if (info.Traget == MenuRank)
				{
					this.gameObject.SetActive(true);
					Price = info.Price;
					PriceValue.text = Price.ToString();
					break;
				}
			}
		});

		ClickBtn.onClick.AddListener(() =>
		{
			if (MainGameData.s_gameData.NowMoney.Value >= Price)
			{
				GameManager.ins.TowerManager.AddTower(MenuRank);
				MainGameData.s_gameData.NowMoney.SetValue(MainGameData.s_gameData.NowMoney.Value - Price);
				MainGameData.GameTouchMap.Value.NowRank = MenuRank;
			}
			MainGameData.GameTouchMap.SetValue(null);
		});

		//ClickBtn.onClick.AddListener(() =>
		//{
		//	GameManager.ins.UpGradeManager.gameObject.SetActive(false);
		//});
	}
}
