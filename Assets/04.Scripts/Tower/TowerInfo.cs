using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
	public Type<ChessRank> NowRank = new Type<ChessRank>(ChessRank.Pawn);
	public int Range;
	public int Damage;
	public float AttackSpeed;
	public List<ChessRank> NextRank;

	public GameObject Target;

	public virtual void Attack()
	{

	}

	public void SetMesh(Mesh Mesh)
	{
		this.GetComponent<MeshFilter>().mesh = Mesh;
	}
}
