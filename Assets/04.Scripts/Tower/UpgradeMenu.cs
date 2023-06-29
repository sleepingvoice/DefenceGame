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
		MainGameData.MapInfo.TouchMap.AddListener((value) =>
		{
			if (value == null)
				return;

			this.gameObject.SetActive(false);

			var list = MainGameData.NextRankList.ReturnNextRank(value.NowRank);

			if (list == null)
			{
				MainGameData.MapInfo.TouchMap.SetValue(null);
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
			if (MainGameData.Money.Value >= Price)
			{
				GameManager.ins.TowerManager.AddTower(MenuRank);
				MainGameData.Money.SetValue(MainGameData.Money.Value - Price);
				MainGameData.MapInfo.TouchMap.Value.NowRank = MenuRank;
			}
			MainGameData.MapInfo.TouchMap.SetValue(null);
		});

		//ClickBtn.onClick.AddListener(() =>
		//{
		//	GameManager.ins.UpGradeManager.gameObject.SetActive(false);
		//});
	}
}
