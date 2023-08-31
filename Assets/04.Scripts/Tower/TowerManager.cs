using System.Collections.Generic;
using Gu;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	public ObjPool TowerObjPool;
	public List<Mesh> TowerMesh;
	public List<GameObject> BulletObj;

	public float Addheigth;
	private MapData _mapInfo = MainGameData.s_mapData;

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
		TempTower.transform.position = _mapInfo.GameTouchMap.Value.CenterPoint + Vector3.up * Addheigth;
		_mapInfo.GameTouchMap.Value.CanBuild = false;

		if (_mapInfo.GameTouchMap.Value.BuildTower != null)
		{
			_mapInfo.GameTouchMap.Value.BuildTower.gameObject.SetActive(false);
		}
		_mapInfo.GameTouchMap.Value.BuildTower = TempTower;
	}
}
