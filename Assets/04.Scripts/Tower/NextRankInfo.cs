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
}
