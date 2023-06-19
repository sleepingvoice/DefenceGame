using System.IO;
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
		LoadJson();
	}

	private void LoadJson()
	{
		MainGameInfo.NextRankList = JsonUtility.FromJson<NextRankList>(File.ReadAllText(Application.streamingAssetsPath + "/NextRankList.json"));
		Debug.Log("타워 로드");
	}

	public void SetArea()
	{
		MapInfo.TouchMap.AddListener((value) => AddTower(value));
	}

	public void AddTower(MapAreaInfo TargetArea)
	{
		if (!TargetArea.CanBuild || !TargetArea.NotMove || MainGameInfo.EditMode)
			return;

		GameObject TempTower = TowerObjPool.GetObject();
		TempTower.GetComponent<TowerInfo>().NowRank.AddListener((value) => TempTower.GetComponent<TowerInfo>().SetMesh(TowerMesh[(int)value]));
		TempTower.GetComponent<TowerInfo>().NowRank.SetValue(ChessRank.Pawn);
		TempTower.transform.position = TargetArea.CenterPoint + Vector3.up * Addheigth;
		TargetArea.CanBuild = false;
	}

}
