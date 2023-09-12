using System;
using System.Collections.Generic;
using UnityEngine;



#region json�� Ŭ����

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
	public Vector3 ShotPos;

	public void InitState(ChessRank Rank)
	{
		State = MainGameData.s_clientData.TowerStateDic[Rank];
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
	public void SetPos(Vector3 Pos);
}

public class PawnTower : TowerBasic, Tower
{
	public PawnTower()
	{
		InitState(ChessRank.Pawn);
	}
	public void SetPos(Vector3 Pos)
	{
		base.ShotPos = Pos;
	}
	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(ShotPos, Target, State.BulletSpeed, () =>
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

	public void SetPos(Vector3 Pos)
	{
		base.ShotPos = Pos;
	}


	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(ShotPos, Target, State.BulletSpeed, () =>
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

	public void SetPos(Vector3 Pos)
	{
		base.ShotPos = Pos;
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(ShotPos, Target, State.BulletSpeed, () =>
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

	public void SetPos(Vector3 Pos)
	{
		base.ShotPos = Pos;
	}

	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(ShotPos, Target, State.BulletSpeed, () =>
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

	public void SetPos(Vector3 Pos)
	{
		base.ShotPos = Pos;
	}


	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(ShotPos, Target, State.BulletSpeed, () =>
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

	public void SetPos(Vector3 Pos)
	{
		base.ShotPos = Pos;
	}


	public void Attack(EnemyInfo Target)
	{
		if (CanAttack)
		{
			NowBullet.ShotBullet(ShotPos, Target, State.BulletSpeed, () =>
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

	public void ShotBullet(Vector3 TransPos,EnemyInfo Target,float BulletSpeed,Action Act)
	{
		var BulletNum = MainGameData.s_clientData.BulletDic[NowRank];
		var NowBullet = GameManager.ins.BulletManager.BulletSet(BulletNum);
		NowBullet.ShotBullet(TransPos, Target, BulletSpeed, Act).Forget();
	}
}