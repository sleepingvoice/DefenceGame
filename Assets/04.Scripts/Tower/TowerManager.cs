using System.Collections.Generic;
using Gu;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	public ObjPool TowerObjPool;
	public List<Mesh> TowerMesh;
	public List<GameObject> BulletObj;

	public float Addheigth;
	private MapData _mapInfo = MainGameData.s_mapInfo;

	private void Awake()
	{
		MainGameData.s_progressValue.AddListener(DisActive);
	}

	private void DisActive(GameProgress progress)
	{
		if (progress != GameProgress.GamePlay)
			TowerObjPool.ReturnObjectAll();
	}

	public void AddTower(ChessRank Rank)
	{
		GameObject TempTower = TowerObjPool.GetObject();
		TempTower.GetComponent<TowerInfo>().SetTower(Rank,TempTower.transform);
		TempTower.GetComponent<TowerInfo>().SetMesh(TowerMesh[(int)Rank]);
		TempTower.transform.position = _mapInfo.TouchMap.Value.CenterPoint + Vector3.up * Addheigth;
		_mapInfo.TouchMap.Value.CanBuild = false;

		if (_mapInfo.TouchMap.Value.BuildTower != null)
		{
			_mapInfo.TouchMap.Value.BuildTower.gameObject.SetActive(false);
		}
		_mapInfo.TouchMap.Value.BuildTower = TempTower;
	}
}
