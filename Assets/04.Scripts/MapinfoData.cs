using System;
using System.Collections.Generic;
using UnityEngine;


public class MapinfoData : MonoBehaviour
{
	public void GetMapInfo()
	{
		Socket.ins.ws_SendMessage("Get_MapList/");
	}

	public void SendMapInfo(MapInfo_Send info)
	{
		Socket.ins.ws_SendMessage("Add_Map/" + JsonUtility.ToJson(info));
	}

}
