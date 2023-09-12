using UnityEngine;

public class UpGradeManager : MonoBehaviour
{
	[HideInInspector]public AreaInfo AreaInfo = null;


	private void Awake()
	{
		MainGameData.GameTouchMap.InsertDic(Init);
	}

	private void Start()
	{
		this.gameObject.SetActive(false);
	}

	private void Init(AreaInfo info)
	{
		if (MainGameData.s_progressMainGame.Value != GameProgress.GamePlay)
			return;

		if (info == null || !info.Notmove)
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
