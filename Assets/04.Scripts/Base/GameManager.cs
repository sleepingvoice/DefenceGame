
using UnityEngine;
using Gu;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager ins => _instance;

	[Header("Game")]
    public AreaManager AreaManager;
	public TowerManager TowerManager;
	public UpGradeManager UpGradeManager;
	public EnemyManager EnemyManager;
	public BulletManager BulletManager;
	public AstarCheck Check;
	public ClickCheck Click;

	[Header("UI")]
	public UIManager_Login UI_Login;
	public UIManager_Lobby Ui_Lobby;
	public UIManager_Game UI_Game;
	public UIManager_EndGame UI_EndGame;
	public UIManager_EditSelect UI_EditSelect;
	public UIManager_Edit UI_Edit;

	[HideInInspector] public Texture2D SaveTex;

	private ServerData _mapInfo = MainGameData.s_serverData;

	private Action _socketAct = null;

	#region 타워 딜레이
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

		MainGameData.s_serverData.NowMap.AddListener(GetData);

		Socket.ins.SocketEventDic.Add("Get_MapList", MapListGet);
	}

	private void GetData(MapInfo info)
	{
		var codiList = JsonUtility.FromJson<CodinateList>(info.codinate);
		var moveList = JsonUtility.FromJson<MapInfoList>(info.movelist);
		var EnemyList = JsonUtility.FromJson<RoundEnemyInfoList>(info.enemyInfo);

		AreaManager.SetMapObj(moveList);

		foreach (var codienate in codiList.NodeList) {
			_mapInfo.Codinate.Add(_mapInfo.AreaDic[codienate]);
		}

		foreach (var enemyinfo in EnemyList.EnemyInfoList)
		{
			_mapInfo.EnemyInfo.Add(enemyinfo.RoundNum, enemyinfo);
		}

	}

	private void Start()
	{
		MainGameData.s_progressMainGame.SetValue(GameProgress.Login);
		GetMapinfo();
	}

	private void Update()
	{
		//시간 체크
		for (int i = 0; i < _delayTower.Count; i++)
		{
			var tmpTower = _delayTower.Dequeue();

			if (tmpTower.DelayTime > nowTime) // 시간이 덜됐으면 반환
				_delayTower.Enqueue(tmpTower);
			else // 다됐으면 공격가능으로 변경
				tmpTower.CanAttack = true;
		}

		nowTime += Time.deltaTime;

		if (_socketAct != null) // 소켓 이벤트 발생
			_socketAct.Invoke();
	}

	public void GetMapinfo()
	{
		Socket.ins.ws_SendMessage("Get_MapList/");
	}

	private void MapListGet(string callback)
	{
		var chagneCallback = "{\"List\":" + callback + "}";
		_socketAct = () =>
		{
			MainGameData.s_serverData.MapinfoSever = JsonUtility.FromJson<MapList>(chagneCallback);

			_socketAct = null;
		};
	}

	public void SetImg(int Num,Action<Texture2D> TexAct)
	{
		StartCoroutine(LoadImg(Num,TexAct));
	}

	public IEnumerator LoadImg(int Num,Action<Texture2D> TexAct)
	{
		DestroyImmediate(SaveTex);
		yield return Socket.ins.GetImg(Num, ((tex) => SaveTex = tex));
		TexAct.Invoke(SaveTex);
	}

	#region Json 값 받아오기

	private void SetJsonValue()
	{
		SetTowerClass();
		LoadTower();
		LoadBulletInfo();
	}

	//타워 능력치 받아오기
	private void SetTowerClass()
	{
		var List = JsonUtility.FromJson<TowerList>(File.ReadAllText(Application.streamingAssetsPath + "/TowerClass.json"));
		foreach (var value in List.Tower)
		{
			MainGameData.s_clientData.TowerStateDic.Add(value.RankValue, value);
		}
	}

	//타워 정보
	private void LoadTower()
	{
		MainGameData.s_clientData.NextRank = JsonUtility.FromJson<NextRankList>(File.ReadAllText(Application.streamingAssetsPath + "/NextRankList.json"));
	}
	
	//총알 정보
	private void LoadBulletInfo()
	{
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Pawn, new KeyValuePair<int, int>(0,0));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Knight, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Bishop, new KeyValuePair<int, int>(0, 1));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Rook, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.Queen, new KeyValuePair<int, int>(0, 0));
		MainGameData.s_clientData.BulletDic.Add(ChessRank.King, new KeyValuePair<int, int>(0, 0));
	}

	#endregion


}
