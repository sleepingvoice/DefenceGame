using System.Collections.Generic;
using Gu;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public ObjPool EnemyPool;

	ServerData Mapinfo = MainGameData.s_serverData;
	GameData GameInfo = MainGameData.s_gameData;

	private void Awake()
	{
		GameInfo.EnemyList.SetValue(new List<EnemyInfo>());
		MainGameData.s_progressMainGame.AddListener(DisActive);
	}

	private void DisActive(GameProgress progress)
	{
		if (progress != GameProgress.GamePlay)
			EnemyPool.ReturnObjectAll();
	}

	public void MakeEnemy(int round)
	{
		GameObject obj = EnemyPool.GetObject();
		obj.transform.position = Mapinfo.Codinate[0].CenterPoint;
		obj.GetComponent<EnemyInfo>().Init(round);
		GameInfo.NowEnemyNum.SetValue(GameInfo.NowEnemyNum.Value + 1);
	}

	public void DieEnemy(EnemyInfo Target)
	{
		EnemyPool.DisableObject(Target.gameObject);
		GameInfo.EnemyList.Value.Remove(Target);
		GameInfo.EnemyList.SetValue(GameInfo.EnemyList.Value);
	}

}
