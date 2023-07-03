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
	}

	public void MakeEnemy(int hp,int speed)
	{
		GameObject obj = EnemyPool.GetObject();
		obj.transform.position = EnemyInfo.TargetList[0].CenterPoint;
		obj.GetComponent<EnemyInfo>().Init(EnemyInfo,hp,speed);
		MainGameData.EnemyNum.SetValue(MainGameData.EnemyNum.Value + 1);
	}

	public void DieEnemy(EnemyInfo Target)
	{
		EnemyPool.DisableObject(Target.gameObject);
		EnemyInfo.EnemyList.Value.Remove(Target);
		EnemyInfo.EnemyList.SetValue(EnemyInfo.EnemyList.Value);
	}

}
