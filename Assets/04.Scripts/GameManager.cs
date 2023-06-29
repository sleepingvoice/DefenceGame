using System.Collections;
using UnityEngine;
using Gu;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public static GameManager ins => Instance;

    public AreaManager AreaManager;
	public TowerManager TowerManager;
	public UpGradeManager UpGradeManager;
	public EnemyManager EnemyManager;
	public BulletManager BulletManager;
	public AstarCheck Check;

	public Camera MainCam;

	private EnemyData EnemyInfo = MainGameData.EnemyInfo;
	private MapData MapInfo = MainGameData.MapInfo;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		SetJsonValue();
		StartCoroutine(MakeEnemy());
	}

	private void Update()
	{
		TouchCheck();
	}

	IEnumerator MakeEnemy()
	{
		for (int i = 0; i < 50; i++)
		{
			EnemyManager.MakeEnemy();
			yield return new WaitForSeconds(1);
		}
	}

	private void TouchCheck()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 6))
			{
				Vector2Int MapNum = new Vector2Int((int)((int)(hit.point.x + AreaManager.AreaSize.x / 2) / MainGameData.MapInfo.AreawidthLength), (int)((int)(hit.point.z + AreaManager.AreaSize.z / 2) / MainGameData.MapInfo.AreaheigthLength));
				MapInfo.TouchMap.SetValue(MapInfo.PointList[MapNum]);
			}
		}
	}

	private void SetJsonValue()
	{
		SetTowerClass();
		LoadTower();
		LoadMapinfo();
		AreaManager.SetList();
		SetMovePos();
		LoadBulletInfo();
	}


	//Ÿ�� �ɷ�ġ �޾ƿ���
	private void SetTowerClass()
	{
		var List = JsonUtility.FromJson<TowerList>(File.ReadAllText(Application.streamingAssetsPath + "/TowerClass.json"));
		foreach (var value in List.Tower)
		{
			MainGameData.TowerState.Add(value.RankValue, value);
		}
	}

	//�����̴� ������ �޾ƿ���
	public void SetMovePos()
	{
		EnemyInfo.TargetList = new List<MapAreaInfo>();
		List<Vector2Int> TargetPos = JsonUtility.FromJson<MovePostion>(File.ReadAllText(Application.streamingAssetsPath + "/MoveCoodinate.json")).MoveCoordinate;
		EnemyInfo.TargetList.Add(MapInfo.PointList[Vector2Int.zero]);
		for (int i = 1; i < TargetPos.Count; i++)
		{
			MapAreaInfo StartInfo = MapInfo.PointList[TargetPos[i - 1]];
			MapAreaInfo EndInfo = MapInfo.PointList[TargetPos[i]];
			EnemyInfo.TargetList.AddRange(Check.PathFindingAstar(StartInfo, EndInfo));
		}

		EnemyInfo.TargetList.AddRange(Check.PathFindingAstar(MapInfo.PointList[TargetPos[TargetPos.Count - 1]], MapInfo.PointList[TargetPos[0]]));
	}

	//Ÿ�� ����
	private void LoadTower()
	{
		MainGameData.NextRankList = JsonUtility.FromJson<NextRankList>(File.ReadAllText(Application.streamingAssetsPath + "/NextRankList.json"));
	}

	//�� ����
	private void LoadMapinfo()
	{
		MapInfo.NotMoveList = JsonUtility.FromJson<NotMovePoint>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/NotMoveList.json"));
	}
	
	//�Ѿ� ����
	private void LoadBulletInfo()
	{
		MainGameData.BulletList.Add(ChessRank.Pawn, new KeyValuePair<int, int>(0,0));
		MainGameData.BulletList.Add(ChessRank.Knight, new KeyValuePair<int, int>(0, 0));
		MainGameData.BulletList.Add(ChessRank.Bishop, new KeyValuePair<int, int>(0, 1));
		MainGameData.BulletList.Add(ChessRank.Rook, new KeyValuePair<int, int>(0, 0));
		MainGameData.BulletList.Add(ChessRank.Queen, new KeyValuePair<int, int>(0, 0));
		MainGameData.BulletList.Add(ChessRank.King, new KeyValuePair<int, int>(0, 0));
	}

}
