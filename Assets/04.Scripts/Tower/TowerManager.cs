using System.IO;
using System.Collections.Generic;
using Gu;
using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour
{
	public ObjPool TowerObjPool;
	public List<Mesh> TowerMesh;

	public float Addheigth;
	private MapData MapInfo = MainGameData.MapInfo;

	private void Awake()
	{
		LoadJson();
	}

	private void LoadJson()
	{
		MainGameData.NextRankList = JsonUtility.FromJson<NextRankList>(File.ReadAllText(Application.streamingAssetsPath + "/NextRankList.json"));
		Debug.Log("타워 로드");
	}

	public void AddTower(ChessRank Rank)
	{
		GameObject TempTower = TowerObjPool.GetObject();
		TempTower.GetComponent<TowerInfo>().SetTower(Rank);
		TempTower.GetComponent<TowerInfo>().SetMesh(TowerMesh[(int)Rank]);
		TempTower.transform.position = MapInfo.TouchMap.Value.CenterPoint + Vector3.up * Addheigth;
		MapInfo.TouchMap.Value.CanBuild = false;

		if (MapInfo.TouchMap.Value.BuildTower != null)
		{
			MapInfo.TouchMap.Value.BuildTower.gameObject.SetActive(false);
		}
		MapInfo.TouchMap.Value.BuildTower = TempTower;
	}

}
