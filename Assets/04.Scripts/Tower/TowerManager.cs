using System.IO;
using System.Collections.Generic;
using Gu;
using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour
{
	public ObjPool TowerObjPool;
	public List<Mesh> TowerMesh;
	public List<GameObject> BulletObj;

	public float Addheigth;
	private MapData MapInfo = MainGameData.MapInfo;

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
