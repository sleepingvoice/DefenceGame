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
	public int RoundNum = 0;
	public int hp = 0;
	public int Speed = 0;
	public int EnemyType = 0;
}
