using System.Collections;
using System.Collections.Generic;
using Gu;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	public ObjPool TowerObjPool;
	public List<Mesh> TowerMesh;

	public float Addheigth;
	private AreaInfo MapInfo = MainGameInfo.MapInfo;

	private void Awake()
	{
		SetArea();
	}

	public void SetArea()
	{
		MapInfo.TouchMap.AddListener((value) => AddTower(value));
	}

	public void AddTower(MapAreaInfo TargetArea)
	{
		if (!TargetArea.CanBuild)
			return;

		GameObject TempTower = TowerObjPool.GetObject();
		TempTower.GetComponent<TowerInfo>().NowRank.AddListener((value) => TempTower.GetComponent<TowerInfo>().SetMesh(TowerMesh[(int)value]));
		TempTower.GetComponent<TowerInfo>().NowRank.SetValue(ChessRank.Pawn);
		TempTower.transform.position = TargetArea.CenterPoint + Vector3.up * Addheigth;
		TargetArea.CanBuild = false;
	}

}
