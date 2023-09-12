using UnityEngine;

public class TowerInfo : MonoBehaviour
{
	public ChessRank NowRank = ChessRank.None;
	public Tower NowTower = null;

	public GameObject Target;
	public TowerCol AttackCol;

	private void Update()
	{
		if (AttackCol.EntryEnemyList.Count > 0)
		{
			NowTower.Attack(AttackCol.EntryEnemyList[0].GetComponent<EnemyInfo>());
		}
	}

	public void SetTower(ChessRank rank, AreaInfo info, Mesh mesh)
	{
		NowRank = rank;

		CreateChessTower TowerCreate = new CreateChessTower();
		NowTower = TowerCreate.CreateTower(rank);
		NowTower.SetPos(info.CenterPoint);

		AttackCol.GetComponent<SphereCollider>().radius = NowTower.ReturnState().State.Range * MainGameData.s_clientData.AreaHeigthLength / 2;
		this.GetComponent<MeshFilter>().mesh = mesh;
	}
}

