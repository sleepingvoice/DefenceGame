using System.Collections.Generic;

public class ClientData
{
	public NextRankList NextRank = new NextRankList();
	public Dictionary<ChessRank, TowerState> TowerStateDic = new Dictionary<ChessRank, TowerState>();
	public Dictionary<ChessRank, KeyValuePair<int, int>> BulletDic = new Dictionary<ChessRank, KeyValuePair<int, int>>();

	public float AreaWidthLength = 0f;
	public float AreaHeigthLength = 0f;

	public int Width = 8;
	public int Hegith = 8;

	public int RoundTime = 20;
	public int RoundEnemy = 30;
	public int LimitEnemy = 50;
}

