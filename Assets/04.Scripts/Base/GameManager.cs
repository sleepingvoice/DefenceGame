
using UnityEngine;
using Gu;
using System.Collections.Generic;
using System.IO;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager ins => _instance;

    public AreaManager AreaManager;
	public TowerManager TowerManager;
	public UpGradeManager UpGradeManager;
	public EnemyManager EnemyManager;
	public BulletManager BulletManager;
	public AstarCheck Check;
	public ClickCheck Click;

	private EnemyData _enemyInfo = MainGameData.s_enemyInfo;
	private MapData _mapInfo = MainGameData.s_mapData;

	#region Ÿ�� ������
	[HideInInspector]public float nowTime = 0f;
	private Queue<TowerBasic> _delayTower = new Queue<TowerBasic>();

	public void AddTower(TowerBasic NewTower)
	{
		_delayTower.Enqueue(NewTower);
	}

	#endregion

	private void Awake()
	{
		if (_instance == null)
			_instance = this;

		MainGameData.s_mapData.NowMap.AddListener(SetData);
	}

	private void SetData(MapInfo info)
	{
		var codiList = JsonUtility.FromJson<CodinateList>(info.codinate);
		var moveList = JsonUtility.FromJson<MapInfoList>(info.movelist);

		AreaManager.SetMapObj(moveList);

		foreach (var codienate in codiList.NodeList) {
			_mapInfo.CodinateDic.Add(codienate, _mapInfo.AreaList[codienate]);
		}
	}

	private void Start()
	{
		MainGameData.s_progressValue.SetValue(GameProgress.Login);
	}

	private void Update()
	{
		//�ð� üũ
		for (int i = 0; i < _delayTower.Count; i++)
		{
			var tmpTower = _delayTower.Dequeue();

			if (tmpTower.DelayTime > nowTime) // �ð��� �������� ��ȯ
				_delayTower.Enqueue(tmpTower);
			else // �ٵ����� ���ݰ������� ����
				tmpTower.CanAttack = true;
		}

		nowTime += Time.deltaTime;
	}

	#region Json �� �޾ƿ���

	private void SetJsonValue()
	{
		SetTowerClass();
		LoadTower();
		SetMovePos();
		LoadBulletInfo();
		LoadEnemyInfo();
	}

	//Ÿ�� �ɷ�ġ �޾ƿ���
	private void SetTowerClass()
	{
		var List = JsonUtility.FromJson<TowerList>(File.ReadAllText(Application.streamingAssetsPath + "/TowerClass.json"));
		foreach (var value in List.Tower)
		{
			MainGameData.s_towerState.Add(value.RankValue, value);
		}
	}

	//�����̴� ������ �޾ƿ���
	public void SetMovePos()
	{
		_enemyInfo.TargetList = new List<AreaInfo>();
		List<Vector2Int> TargetPos = JsonUtility.FromJson<MovePostion>(File.ReadAllText(Application.streamingAssetsPath + "/MoveCoodinate.json")).MoveCoordinate;
		_enemyInfo.TargetList.Add(_mapInfo.CodinateDic[Vector2Int.zero]);
		for (int i = 1; i < TargetPos.Count; i++)
		{
			AreaInfo StartInfo = _mapInfo.CodinateDic[TargetPos[i - 1]];
			AreaInfo EndInfo = _mapInfo.CodinateDic[TargetPos[i]];
			_enemyInfo.TargetList.AddRange(Check.PathFindingAstar(StartInfo, EndInfo));
		}

		_enemyInfo.TargetList.AddRange(Check.PathFindingAstar(_mapInfo.CodinateDic[TargetPos[TargetPos.Count - 1]], _mapInfo.CodinateDic[TargetPos[0]]));
	}

	//Ÿ�� ����
	private void LoadTower()
	{
		MainGameData.s_nextRankList = JsonUtility.FromJson<NextRankList>(File.ReadAllText(Application.streamingAssetsPath + "/NextRankList.json"));
	}
	
	//�Ѿ� ����
	private void LoadBulletInfo()
	{
		MainGameData.s_bulletList.Add(ChessRank.Pawn, new KeyValuePair<int, int>(0,0));
		MainGameData.s_bulletList.Add(ChessRank.Knight, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_bulletList.Add(ChessRank.Bishop, new KeyValuePair<int, int>(0, 1));
		MainGameData.s_bulletList.Add(ChessRank.Rook, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_bulletList.Add(ChessRank.Queen, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_bulletList.Add(ChessRank.King, new KeyValuePair<int, int>(0, 0));
	}

	private void LoadEnemyInfo()
	{
		var info = JsonUtility.FromJson<RoundEnemyInfoList>(File.ReadAllText(Application.streamingAssetsPath + "/RoundEnemyInfo.json"));
		foreach (var infoValue in info.EnemyInfoList) 
		{
			_enemyInfo.EnemyInfo.Add(infoValue.RoundNum, infoValue);
		}
	}

	#endregion


}
