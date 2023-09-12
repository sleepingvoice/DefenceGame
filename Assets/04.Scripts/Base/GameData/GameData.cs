
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
	public Type<int> NowMoney = new Type<int>(500);
	public Type<int> NowEnemyNum = new Type<int>(0);
	public Type<int> NowRoundTime = new Type<int>(0);
	public Type<int> NowRound = new Type<int>(0);

	public List<GameObject> BuildTowerInfoList = new List<GameObject>();
	public List<AreaInfo> MoveList = new List<AreaInfo>();

	public Type<List<EnemyInfo>> EnemyList = new Type<List<EnemyInfo>>();
}
