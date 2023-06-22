using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
	public ChessRank MenuRank;
	public int Price;

	public Button ClickBtn;

	private void Awake()
	{
		MainGameInfo.MapInfo.TouchMap.AddListener((value) =>
		{
			this.gameObject.SetActive(false);
			var list = MainGameInfo.NextRankList.ReturnNextRank(value.NowRank);
			foreach (var info in list)
			{
				if (info.Traget == MenuRank)
				{
					this.gameObject.SetActive(true);
					Price = info.Price;
					break;
				}
			}
		});

		//ClickBtn.onClick.AddListener(() =>
		//{
		//	GameManager.ins.UpGradeManager.gameObject.SetActive(false);
		//});
	}
}
