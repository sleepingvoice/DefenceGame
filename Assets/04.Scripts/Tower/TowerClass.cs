using System;
using System.Collections.Generic;
using JetBrains.Annotations;

[Serializable]
public class NextRankInfo
{
	public ChessRank Traget;
	public int Price;
}

[Serializable]
public class NextRank
{
	public ChessRank TargetRank;
	public List<NextRankInfo> NextRankChess = new List<NextRankInfo>();
}

[Serializable]
public class NextRankList
{
	public List<NextRank> RankList = new List<NextRank>();

	public List<NextRankInfo> ReturnNextRank(ChessRank Rank)
	{
		foreach (var value in RankList)
		{
			if (value.TargetRank == Rank)
				return value.NextRankChess;
		}
		return null;
	}
}

[Serializable]
public class TowerState
{
	public ChessRank RankValue = ChessRank.Pawn;
	public int Range;
	public int Damage;
	public float AttackSpeed;
}

[Serializable]
public class TowerList
{
	public List<TowerState> Tower = new List<TowerState>();
}
