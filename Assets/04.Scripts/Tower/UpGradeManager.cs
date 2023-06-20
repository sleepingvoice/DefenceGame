using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeManager : MonoBehaviour
{
	private AreaInfo MapInfo = MainGameInfo.MapInfo;

	private MapAreaInfo AreaInfo = null;


	private void Awake()
	{
		MapInfo.TouchMap.AddListener(Init);
		this.gameObject.SetActive(false);
	}

	private void Init(MapAreaInfo info)
	{
		this.gameObject.SetActive(true);
		AreaInfo = info;
	}
}
