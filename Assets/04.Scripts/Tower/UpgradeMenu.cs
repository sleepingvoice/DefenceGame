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

	private void Awake()
	{
		MainGameData.s_mapInfo.TouchMap.AddListener((value) =>
		{
			if (value == null)
				return;

			this.gameObject.SetActive(false);

			var list = MainGameData.s_nextRankList.ReturnNextRank(value.NowRank);

			if (list == null)
			{
				MainGameData.s_mapInfo.TouchMap.SetValue(null);
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
			if (MainGameData.s_money.Value >= Price)
			{
				GameManager.ins.TowerManager.AddTower(MenuRank);
				MainGameData.s_money.SetValue(MainGameData.s_money.Value - Price);
				MainGameData.s_mapInfo.TouchMap.Value.NowRank = MenuRank;
			}
			MainGameData.s_mapInfo.TouchMap.SetValue(null);
		});

		//ClickBtn.onClick.AddListener(() =>
		//{
		//	GameManager.ins.UpGradeManager.gameObject.SetActive(false);
		//});
	}
}
