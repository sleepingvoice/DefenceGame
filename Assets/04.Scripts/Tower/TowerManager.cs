using System.Collections.Generic;
using Gu;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	public ObjPool TowerObjPool;
	public List<Mesh> TowerMesh;
	public List<GameObject> BulletObj;

	public float Addheigth;

	private void Awake()
	{
		MainGameData.s_progressMainGame.AddListener(DisActive);
	}

	private void DisActive(GameProgress progress)
	{
		if (progress != GameProgress.GamePlay)
			TowerObjPool.ReturnObjectAll();
	}

	public void AddTower(ChessRank Rank, AreaInfo info)
	{
		GameObject TempTower = TowerObjPool.GetObject();
		TempTower.GetComponent<TowerInfo>().SetTower(Rank, info, TowerMesh[(int)Rank]);
		TempTower.transform.position = MainGameData.GameTouchMap.Value.CenterPoint + Vector3.up * Addheigth;
		info.CanBuild = false;

		if (info.BuildTower != null)
		{
			TowerObjPool.DisableObject(info.BuildTower);
		}
		info.BuildTower = TempTower;

		MainGameData.s_gameData.BuildTowerInfoList.Add(TempTower);
	}
}
