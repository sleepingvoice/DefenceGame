using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



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
	public float BulletSpeed;
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

#region Tower

public class TowerBasic
{
	public bool CanAttack = true;
	public float DelayTime = 0f;

	public TowerState State;
	public BulletCreate NowBullet;
	public Transform Trans;

	public void InitState(ChessRank Rank)
	{
		State = MainGameData.s_towerState[Rank];
		NowBullet = new BulletCreate(Rank);
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
	public void SetPos(Transform Trans);
}

public class PawnTower : TowerBasic, Tower
{
	public PawnTower()
	{
		InitState(ChessRank.Pawn);
	}

	public void SetPos(Transform trans)
	{
		Trans = trans;
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(Trans, Target, State.BulletSpeed, () =>
			   {
				   Target.Demaged(State.Damage);
			   });

			CanAttack = false;
			DelayTime = GameManager.ins.nowTime + State.AttackSpeed;
			GameManager.ins.AddTower(this);
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

	public void SetPos(Transform trans)
	{
		Trans = trans;
	}


	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(Trans, Target, State.BulletSpeed, () =>
			{
				Target.Demaged(State.Damage);
			});

			CanAttack = false;
			DelayTime = GameManager.ins.nowTime + State.AttackSpeed;
			GameManager.ins.AddTower(this);
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

	public void SetPos(Transform trans)
	{
		Trans = trans;
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(Trans, Target, State.BulletSpeed, () =>
			{
				Target.Demaged(State.Damage);
			});

			CanAttack = false;
			DelayTime = GameManager.ins.nowTime + State.AttackSpeed;
			GameManager.ins.AddTower(this);
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

	public void SetPos(Transform trans)
	{
		Trans = trans;
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(Trans, Target, State.BulletSpeed, () =>
			{
				Target.Demaged(State.Damage);
			});

			CanAttack = false;
			DelayTime = GameManager.ins.nowTime + State.AttackSpeed;
			GameManager.ins.AddTower(this);
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

	public void SetPos(Transform trans)
	{
		Trans = trans;
	}


	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(Trans, Target, State.BulletSpeed, () =>
			{
				Target.Demaged(State.Damage);
			});

			CanAttack = false;
			DelayTime = GameManager.ins.nowTime + State.AttackSpeed;
			GameManager.ins.AddTower(this);
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

	public void SetPos(Transform trans)
	{
		Trans = trans;
	}


	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(Trans, Target, State.BulletSpeed, () =>
			{
				Target.Demaged(State.Damage);
			});

			CanAttack = false;
			DelayTime = GameManager.ins.nowTime + State.AttackSpeed;
			GameManager.ins.AddTower(this);
		}
	}

	public TowerBasic ReturnState()
	{
		return this;
	}
}

#endregion

public class BulletCreate
{
	public ChessRank NowRank;

	public BulletCreate(ChessRank Rank)
	{
		NowRank = Rank;
	}

	public void ShotBullet(Transform Trans,EnemyInfo Target,float BulletSpeed,Action Act)
	{
		var BulletNum = MainGameData.s_bulletList[NowRank];
		var NowBullet = GameManager.ins.BulletManager.BulletSet(BulletNum);
		NowBullet.ShotBullet(Trans.transform.position, Target, BulletSpeed, Act).Forget();
	}
}