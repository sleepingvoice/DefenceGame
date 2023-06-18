using UnityEngine;

public class MapAreaInfo
{
	public MapAreaInfo(Vector2Int _nodeNum, Vector3 _nodePoint, Vector3 _centerPoint,bool _notMove)
	{
		NodeNum = _nodeNum;
		NodePoint = _nodePoint;
		CenterPoint = _centerPoint;
		NotMove = _notMove;
	}

	public Vector2Int NodeNum = new Vector2Int(0,0);
	public Vector3 NodePoint = Vector3.zero;
	public Vector3 CenterPoint = Vector3.zero;
	public bool CanBuild = true;
	public bool NotMove = false;
}
