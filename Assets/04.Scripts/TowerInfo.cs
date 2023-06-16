using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour
{
	public Type<ChessRank> NowRank = new Type<ChessRank>(ChessRank.Pawn);

	public void SetMesh(Mesh Mesh)
	{
		this.GetComponent<MeshFilter>().mesh = Mesh;
	}
}
