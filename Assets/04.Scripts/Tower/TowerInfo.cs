using UnityEngine;


public class TowerInfo : MonoBehaviour
{
	public ChessRank NowRank = ChessRank.None;
	public Tower NowTower;

	public GameObject Target;
	public GameObject AttackCol;

	public void SetTower(ChessRank Rank)
	{
		NowRank = Rank;

		CreateChessTower TowerCreate = new CreateChessTower();
		NowTower = TowerCreate.CreateTower(Rank);
		NowTower.InitState();
		AttackCol.GetComponent<SphereCollider>().radius = NowTower.ReturnState().State.Range * MainGameInfo.MapInfo.Hegith / 2;
	}

	public void SetMesh(Mesh Mesh)
	{
		this.GetComponent<MeshFilter>().mesh = Mesh;
	}
}

