using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TowerInfo : MonoBehaviour
{
	public Type<ChessRank> NowRank = new Type<ChessRank>(ChessRank.None);
	public List<NextRankInfo> NextRank;

	public GameObject Target;
	public GameObject AttackCol;

	private TowerState NowState = new TowerState();

	public void SetTower(ChessRank Rank)
	{
		NowRank.SetValue(Rank);
		NextRank = MainGameInfo.NextRankList.ReturnNextRank(Rank);
		NowState = MainGameInfo.TowerState[Rank];
		AttackCol.GetComponent<SphereCollider>().radius = NowState.Range * MainGameInfo.MapInfo.Hegith / 2;
	}

	public virtual void Attack()
	{

	}

	public void SetMesh(Mesh Mesh)
	{
		this.GetComponent<MeshFilter>().mesh = Mesh;
	}
}
