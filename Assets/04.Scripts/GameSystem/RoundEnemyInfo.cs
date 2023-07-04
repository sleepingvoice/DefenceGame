using System;
using System.Collections.Generic;

[Serializable]
public class RoundEnemyInfoList
{
	public List<RoundEnemyInfo> EnemyInfoList = new List<RoundEnemyInfo>();
}

[Serializable]
public class RoundEnemyInfo
{
	public int RoundNum;
	public int hp;
	public int Speed;
	public int EnemyType;
}
