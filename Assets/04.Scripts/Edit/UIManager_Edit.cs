using UnityEngine;

public class UIManager_Edit : MonoBehaviour
{
	[HideInInspector] public CodinateList EditNode = new CodinateList();
	[HideInInspector] public RoundEnemyInfoList EnemyList = new RoundEnemyInfoList();

	private void Awake()
	{
		MainGameData.s_progressMainGame.AddListener(Active);
	}

	private void Start()
	{
		MainGameData.s_progressEdit.SetValue(EditProgrss.main);
	}

	private void Active(GameProgress progress)
	{
		this.gameObject.SetActive(progress == GameProgress.Edit);
		if (progress == GameProgress.Edit)
		{
			GameManager.ins.AreaManager.SetMapObj(); // √ ±‚»≠
		}
	}


}
