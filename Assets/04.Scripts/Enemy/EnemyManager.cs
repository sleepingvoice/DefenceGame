using Gu;
using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

public class EnemyManager : MonoBehaviour
{
	public ObjPool EnemyPool;
	public AstarCheck Check;


	EnemyData EnemyInfo = MainGameData.EnemyInfo;
	MapData MapInfo = MainGameData.MapInfo;

	private void Awake()
	{
		EnemyInfo.EnemyList.SetValue(new List<EnemyInfo>());
	}

	private void Start()
	{
		SetMovePos();
		MakeEnemy();
	}

	public void SetMovePos()
	{
		EnemyInfo.TargetList = new List<MapAreaInfo>();
		List<Vector2Int> TargetPos = JsonUtility.FromJson<MovePostion>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/MoveCoodinate.json")).MoveCoordinate;
		EnemyInfo.TargetList.Add(MapInfo.PointList[Vector2Int.zero]);
		for (int i = 1; i < TargetPos.Count; i++)
		{
			MapAreaInfo StartInfo = MapInfo.PointList[TargetPos[i - 1]];
			MapAreaInfo EndInfo = MapInfo.PointList[TargetPos[i]];
			EnemyInfo.TargetList.AddRange(Check.PathFindingAstar(StartInfo, EndInfo));
		}

		EnemyInfo.TargetList.AddRange(Check.PathFindingAstar(MapInfo.PointList[TargetPos[TargetPos.Count-1]], MapInfo.PointList[TargetPos[0]]));

	}

	public void MakeEnemy()
	{
		GameObject obj = EnemyPool.GetObject();
		obj.transform.position = EnemyInfo.TargetList[0].CenterPoint;
		obj.GetComponent<EnemyInfo>().Init(EnemyInfo);
		MainGameData.EnemyNum.SetValue(MainGameData.EnemyNum.Value + 1);
	}

	public void DieEnemy(EnemyInfo Target)
	{
		EnemyPool.DisableObject(Target.gameObject);
		EnemyInfo.EnemyList.Value.Remove(Target);
		EnemyInfo.EnemyList.SetValue(EnemyInfo.EnemyList.Value);
	}

}
