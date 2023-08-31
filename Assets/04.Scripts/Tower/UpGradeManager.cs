using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeManager : MonoBehaviour
{
	private MapData MapInfo = MainGameData.s_mapData;

	private AreaInfo AreaInfo = null;


	private void Awake()
	{
		MapInfo.GameTouchMap.InsertDic(Init);
	}

	private void Start()
	{
		this.gameObject.SetActive(false);
	}

	private void Init(AreaInfo info)
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
