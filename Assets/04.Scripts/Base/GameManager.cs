
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Gu;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager ins => _instance;

	public Camera MainCam;

	[Header("Game")]
	public AreaManager AreaManager;
	public TowerManager TowerManager;
	public UpGradeManager UpGradeManager;
	public EnemyManager EnemyManager;
	public BulletManager BulletManager;
	public AstarCheck Check;
	public ClickCheck Click;

	[Header("UI")]
	public UIManager_Lobby Ui_Lobby;
	public UIManager_Game UI_Game;
	public UIManager_EndGame UI_EndGame;
	public UIManager_EditSelect UI_EditSelect;
	public UIManager_Edit UI_Edit;

	[HideInInspector] public Texture2D SaveTex;
	[HideInInspector] public Queue<Action> SoketAct = new Queue<Action>();


	private ServerData _mapInfo = MainGameData.s_serverData;

	#region Ÿ�� ������
	[HideInInspector] public float nowTime = 0f;
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

		MainGameData.s_serverData.NowMap.AddListener(GetData);

		Socket.ins.SocketEventDic.Add("Get_MapList", MapListGet);

		SetJsonValue();
	}

	private void GetData(MapInfo info)
	{
		var codiList = JsonUtility.FromJson<CodinateList>(info.codinate);
		var moveList = JsonUtility.FromJson<MapInfoList>(info.movelist);
		var EnemyList = JsonUtility.FromJson<RoundEnemyInfoList>(info.enemyInfo);

		AreaManager.SetMapObj(moveList);
		_mapInfo.Codinate = new List<AreaInfo>();
		_mapInfo.EnemyInfo = new Dictionary<int, RoundEnemyInfo>();

		foreach (var codienate in codiList.NodeList)
		{
			_mapInfo.Codinate.Add(_mapInfo.AreaDic[codienate]);
		}

		foreach (var enemyinfo in EnemyList.EnemyInfoList)
		{
			_mapInfo.EnemyInfo.Add(enemyinfo.RoundNum, enemyinfo);
		}

		MainGameData.s_gameData.MoveList = new List<AreaInfo>();
		MainGameData.s_gameData.MoveList.Add(_mapInfo.Codinate[0]);

		for (int i = 0; i < _mapInfo.Codinate.Count - 1; i++)
		{
			MainGameData.s_gameData.MoveList.AddRange(Check.PathFindingAstar(_mapInfo.Codinate[i], _mapInfo.Codinate[i + 1]));
		}

		MainGameData.s_gameData.MoveList.AddRange(Check.PathFindingAstar(_mapInfo.Codinate[_mapInfo.Codinate.Count - 1], _mapInfo.Codinate[0]));
		AreaManager.gameObject.GetComponent<ShowMap>().SetCodinateColor(_mapInfo.Codinate);

	}

	private void Start()
	{
		MainGameData.s_progressMainGame.SetValue(GameProgress.Login);
		MainGameData.s_progressLogin.SetValue(LoginProgress.selectLogin);
		GetMapinfo();
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

		if (SoketAct.Count != 0) // ���� �̺�Ʈ �߻�
		{
			SoketAct.Dequeue().Invoke();
		}
	}

	public void GameStart()
	{
		MainGameData.s_progressMainGame.SetValue(GameProgress.GamePlay);

		MainGameData.s_gameData.NowEnemyNum.SetValue(0);
		MainGameData.s_gameData.NowMoney.SetValue(500);
		MainGameData.s_gameData.NowRound.SetValue(1);
	}

	public void GetMapinfo()
	{
		Debug.Log("����Ʈ ����");
		Socket.ins.ws_SendMessage("Get_MapList/");
	}

	private void MapListGet(string callback)
	{
		var chagneCallback = "{\"List\":" + callback + "}";
		SoketAct.Enqueue(() => MainGameData.s_serverData.MapinfoSever = JsonUtility.FromJson<MapList>(chagneCallback));
	}

	#region �̹��� Ȯ��

	public void SetImg()
	{
		StartCoroutine(this.gameObject.GetComponent<CaptureImg>().CaptureCam());
	}

	#endregion

	#region Json �� �޾ƿ���

	private void SetJsonValue()
	{
		SetTowerClass();
		LoadTower();
		LoadBulletInfo();
	}

	//Ÿ�� �ɷ�ġ �޾ƿ���
	private void SetTowerClass()
	{
		StartCoroutine(utility.GetJson(Application.streamingAssetsPath + "/TowerClass.json", (str) =>
		 {
			 var List = JsonUtility.FromJson<TowerList>(str);
			 foreach (var value in List.Tower)
			 {
				 MainGameData.s_clientData.TowerStateDic.Add(value.RankValue, value);
			 }
		 }));
	}

	//Ÿ�� ����
	private void LoadTower()
	{
		StartCoroutine(utility.GetJson(Application.streamingAssetsPath + "/NextRankList.json", (str) =>
		{
			MainGameData.s_clientData.NextRank = JsonUtility.FromJson<NextRankList>(str);
		}));
	}

	//�Ѿ� ����
	private void LoadBulletInfo()
	{
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Pawn, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Knight, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Bishop, new KeyValuePair<int, int>(0, 1));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Rook, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Queen, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.King, new KeyValuePair<int, int>(0, 0));
	}

	#endregion


}
