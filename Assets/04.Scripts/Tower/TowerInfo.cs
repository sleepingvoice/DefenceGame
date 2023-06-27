using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
	public ChessRank NowRank = ChessRank.None;
	public Tower NowTower;

	public GameObject Target;
	public TowerCol AttackCol;

	private void Update()
	{
		if (AttackCol.EntryEnemyList.Count > 0)
		{
			NowTower.Attack(AttackCol.EntryEnemyList[0].GetComponent<EnemyInfo>());
		}
	}

	public void SetTower(ChessRank Rank)
	{
		NowRank = Rank;

		CreateChessTower TowerCreate = new CreateChessTower();
		NowTower = TowerCreate.CreateTower(Rank);
		AttackCol.GetComponent<SphereCollider>().radius = NowTower.ReturnState().State.Range * MainGameData.MapInfo.AreaheigthLength / 2;
	}

	public void SetMesh(Mesh Mesh)
	{
		this.GetComponent<MeshFilter>().mesh = Mesh;
	}
}

