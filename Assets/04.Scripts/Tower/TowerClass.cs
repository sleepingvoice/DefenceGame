using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


#region json용 클래스

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
	public ChessRank RankValue = ChessRank.None;
	public int Range;
	public int Damage;
	public float AttackSpeed;
}

[Serializable]
public class TowerList
{
	public List<TowerState> Tower = new List<TowerState>();
}

#endregion

public class TowerBasic
{
	public TowerState State;
	public GameObject Bullet;
}

public interface TowerCreate
{
	public Tower CreateTower(ChessRank Rank);
}

public class CreateChessTower : TowerCreate
{
	public Tower CreateTower(ChessRank Rank)
	{
		switch (Rank)
		{
			case ChessRank.Pawn:
				return new PawnTower();
			case ChessRank.Knight:
				return new KnightTower();
			case ChessRank.Bishop:
				return new BishopTower();
			case ChessRank.Rook:
				return new RookTower();
			case ChessRank.Queen:
				return new QueenTower();
			case ChessRank.King:
				return new KingTower();
		}
		return null;
	}
}

public interface Tower
{
	public void InitState();
	public void Attack(EnemyInfo Target);
	public TowerBasic ReturnState();
}

public class PawnTower : TowerBasic, Tower
{
	public void InitState()
	{
		State = MainGameInfo.TowerState[ChessRank.Pawn];
	}

	public void Attack(EnemyInfo Target)
	{
		Target.Hp -= State.Damage;
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class KnightTower : TowerBasic, Tower
{
	public void InitState()
	{
		State = MainGameInfo.TowerState[ChessRank.Knight];
	}

	public void Attack(EnemyInfo Target)
	{
		Target.Hp -= State.Damage;
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class BishopTower : TowerBasic, Tower
{
	public int SlowTime;

	public void InitState()
	{
		State = MainGameInfo.TowerState[ChessRank.Bishop];
	}

	public void Attack(EnemyInfo Target)
	{
		Target.Hp -= State.Damage;
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class RookTower : TowerBasic, Tower
{
	public void InitState()
	{
		State = MainGameInfo.TowerState[ChessRank.Rook];
	}

	public void Attack(EnemyInfo Target)
	{
		Target.Hp -= State.Damage;
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class QueenTower : TowerBasic, Tower
{
	public void InitState()
	{
		State = MainGameInfo.TowerState[ChessRank.Queen];
	}

	public void Attack(EnemyInfo Target)
	{
		Target.Hp -= State.Damage;
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class KingTower : TowerBasic, Tower
{
	public void InitState()
	{
		State = MainGameInfo.TowerState[ChessRank.King];
	}

	public void Attack(EnemyInfo Target)
	{
		Target.Hp -= State.Damage;
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}


