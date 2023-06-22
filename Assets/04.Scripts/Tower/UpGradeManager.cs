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
	}

	private void Start()
	{
		this.gameObject.SetActive(false);
	}

	private void Init(MapAreaInfo info)
	{
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
