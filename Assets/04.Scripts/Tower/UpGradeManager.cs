using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeManager : MonoBehaviour
{
	private AreaInfo MapInfo = MainGameInfo.MapInfo;

	private MapAreaInfo AreaInfo = null;

	private void Start()
	{
		MapInfo.TouchMap.AddListener(Init);
	}

	private void Init(MapAreaInfo info)
	{
		this.gameObject.SetActive(true);
		AreaInfo = info;
	}
}
