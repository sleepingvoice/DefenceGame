using System.Collections.Generic;
using Gu;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	public ObjPool TowerObjPool;
	public List<Mesh> TowerMesh;
	public List<GameObject> BulletObj;

	public float Addheigth;
	private ServerData _mapInfo = MainGameData.s_serverData;

	private void Awake()
	{
		MainGameData.s_progressMainGame.AddListener(DisActive);
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
		TempTower.transform.position = MainGameData.GameTouchMap.Value.CenterPoint + Vector3.up * Addheigth;
		MainGameData.GameTouchMap.Value.CanBuild = false;

		if (MainGameData.GameTouchMap.Value.BuildTower != null)
		{
			MainGameData.GameTouchMap.Value.BuildTower.gameObject.SetActive(false);
		}
		MainGameData.GameTouchMap.Value.BuildTower = TempTower;
	}
}
