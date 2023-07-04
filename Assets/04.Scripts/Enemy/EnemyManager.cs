using Gu;
using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

public class EnemyManager : MonoBehaviour
{
	public ObjPool EnemyPool;

	EnemyData EnemyInfo = MainGameData.EnemyInfo;

	private void Awake()
	{
		EnemyInfo.EnemyList.SetValue(new List<EnemyInfo>());
		MainGameData.ProgressValue.AddListener(DisActive);
	}

	private void DisActive(GameProgress progress)
	{
		if (progress != GameProgress.GamePlay)
			EnemyPool.ReturnObjectAll();
	}

	public void MakeEnemy(int round)
	{
		GameObject obj = EnemyPool.GetObject();
		obj.transform.position = EnemyInfo.TargetList[0].CenterPoint;
		obj.GetComponent<EnemyInfo>().Init(EnemyInfo,round);
		MainGameData.EnemyNum.SetValue(MainGameData.EnemyNum.Value + 1);
	}

	public void DieEnemy(EnemyInfo Target)
	{
		EnemyPool.DisableObject(Target.gameObject);
		EnemyInfo.EnemyList.Value.Remove(Target);
		EnemyInfo.EnemyList.SetValue(EnemyInfo.EnemyList.Value);
	}

}
