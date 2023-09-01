using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


#region 맵 전체 정보

[Serializable]
public class MapList
{
	public List<MapInfo> List = new List<MapInfo>();
}

[Serializable]
public class MapInfo
{
	public int mapid;
	public int userId;
	public string codinate;
	public string movelist;
	public string enemyInfo;
	public string mapName;
	public byte[] mapImg;
}

[Serializable]
public class MapInfo_Send
{
	public int userId;
	public string codinate;
	public string movelist;
	public string enemyInfo;
	public string mapName;
	public byte[] mapImg;
}

#endregion

#region 지점 정보

[Serializable]
public class AreaInfo
{
	public AreaInfo(Vector2Int nodeNum, Vector3 nodePoint, Vector3 centerPoint, bool notMove, GameObject outlineObj)
	{
		_nodeNum = nodeNum;
		_nodePoint = nodePoint;
		_centerPoint = centerPoint;
		Notmove = notMove;

		outlineObj.transform.position = centerPoint + Vector3.up * 0.1f;
		_outlineObj = outlineObj;
		_outlineObj.SetActive(false);
	}

	private Vector2Int _nodeNum = new Vector2Int(0, 0);
	public Vector2Int NodeNum
	{
		get
		{
			return _nodeNum;
		}
	}

	private Vector3 _nodePoint = Vector3.zero;
	public Vector3 NodePoint
	{
		get
		{
			return _nodePoint;
		}
	}

	private Vector3 _centerPoint = Vector3.zero;
	public Vector3 CenterPoint
	{
		get
		{
			return _centerPoint;
		}
	}

	public bool Notmove = false;

	private GameObject _outlineObj;
	public GameObject OutLineObj
	{
		get
		{
			return _outlineObj;
		}
	}

	public GameObject BuildTower = null;
	public bool CanBuild = true;
	public ChessRank NowRank = ChessRank.None;
}

#endregion

#region 맵 저장용 클래스

[Serializable]
public class MapAreaInfoSave
{
	public Vector2Int NodeNum;
	public bool NotMove;
}

[Serializable]
public class MapInfoList
{
	public List<MapAreaInfoSave> InfoList = new List<MapAreaInfoSave>();
}

#endregion

#region Codinate 저장

[Serializable]
public class CodinateList
{
	public List<Vector2Int> NodeList = new List<Vector2Int>();
}

#endregion

