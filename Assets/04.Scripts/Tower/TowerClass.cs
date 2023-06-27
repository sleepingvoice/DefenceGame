using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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

[Serializable]
public class MovePostion
{
	public List<Vector2Int> MoveCoordinate;
}

#endregion

public class TowerBasic
{
	public bool CanAttack = true;
	public TowerState State;
	public GameObject Bullet;

	private float delayTime = 0f;

	public void SetBulletObj(GameObject obj)
	{
		Bullet = obj;
	}

	public void InitState(ChessRank Rank)
	{
		State = MainGameData.TowerState[Rank];
	}

	public async UniTaskVoid WaitTime()
	{
		CanAttack = false;
		while (delayTime < State.AttackSpeed)
		{
			delayTime += Time.deltaTime;
			await UniTask.DelayFrame(1);
		}
		delayTime = 0f;
		CanAttack = true;
	}

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
	public void Attack(EnemyInfo Target);
	public TowerBasic ReturnState();
}

public class PawnTower : TowerBasic, Tower
{
	public PawnTower()
	{
		InitState(ChessRank.Pawn);
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			Target.Demaged(State.Damage);
			WaitTime().Forget();
			Debug.Log("때림");
		}
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class KnightTower : TowerBasic, Tower
{
	public KnightTower()
	{
		InitState(ChessRank.Knight);
	}


	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			Target.Hp -= State.Damage;
			WaitTime().Forget();
		}
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class BishopTower : TowerBasic, Tower
{
	public int SlowTime;

	public BishopTower()
	{
		InitState(ChessRank.Bishop);
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			Target.Hp -= State.Damage;
			WaitTime().Forget();
		}
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class RookTower : TowerBasic, Tower
{
	public RookTower()
	{
		InitState(ChessRank.Rook);
	}


	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			Target.Hp -= State.Damage;
			WaitTime().Forget();
		}
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class QueenTower : TowerBasic, Tower
{
	public QueenTower()
	{
		InitState(ChessRank.Queen);
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			Target.Hp -= State.Damage;
			WaitTime().Forget();
		}
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

public class KingTower : TowerBasic, Tower
{
	public KingTower()
	{
		InitState(ChessRank.King);
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			Target.Hp -= State.Damage;
			WaitTime().Forget();
		}
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}


