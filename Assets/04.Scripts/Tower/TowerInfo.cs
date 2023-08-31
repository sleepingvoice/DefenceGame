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

	public void SetTower(ChessRank rank, Transform trans)
	{
		NowRank = rank;

		CreateChessTower TowerCreate = new CreateChessTower();
		NowTower = TowerCreate.CreateTower(rank);
		NowTower.SetPos(trans);

		AttackCol.GetComponent<SphereCollider>().radius = NowTower.ReturnState().State.Range * MainGameData.s_mapData.AreaheigthLength / 2;
	}

	public void SetMesh(Mesh mesh)
	{
		this.GetComponent<MeshFilter>().mesh = mesh;
	}
}

