using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapList
{
	public List<GetMapInfo> List = new List<GetMapInfo>();
}

[Serializable]
public class GetMapInfo
{
	public int mapid;
	public int userId;
	public string codinate;
	public string movelist;
	public string enemyInfo;
}

[Serializable]
public class SendMapInfo
{
	public int userId;
	public string codinate;
	public string movelist;
	public string enemyInfo;
}

public class Mapinfo : MonoBehaviour
{
	private Action _socketAct = null;

	private void Start()
	{
		Socket.ins.SocketEventDic.Add("Get_MapList", MapListGet);
	}


	private void Update()
	{
		if (_socketAct != null)
		{
			_socketAct.Invoke();
			_socketAct = null;
		}
	}

	public void GetMapInfo()
	{
		Socket.ins.ws_SendMessage("Get_MapList/");
	}

	public void SendMapInfo(SendMapInfo info)
	{
		Socket.ins.ws_SendMessage("Add_Map/" + JsonUtility.ToJson(info));
	}

	private void MapListGet(string callback)
	{
		var chagneCallback = "{\"List\":" + callback + "}";
		_socketAct = () =>
		{
			var obj = JsonUtility.FromJson<MapList>(chagneCallback);
			
			//맵인포 넣어주는 기능 추가

			_socketAct = null;
		};
	}
}
