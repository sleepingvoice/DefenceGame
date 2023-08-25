using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeManager : MonoBehaviour
{
	private MapData MapInfo = MainGameData.s_mapInfo;

	private MapAreaInfo AreaInfo = null;


	private void Awake()
	{
		MapInfo.TouchMap.InsertDic(Init);
	}

	private void Start()
	{
		this.gameObject.SetActive(false);
	}

	private void Init(MapAreaInfo info)
	{
		if (MainGameData.s_progressValue.Value != GameProgress.GamePlay)
			return;

		if (info == null || !info.NotMove)
		{
			this.gameObject.SetActive(false);
		}
		else
		{
			this.gameObject.SetActive(true);
			AreaInfo = info;
		}
	}
}
