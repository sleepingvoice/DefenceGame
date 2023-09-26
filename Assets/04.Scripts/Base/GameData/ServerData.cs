using System.Collections.Generic;
using UnityEngine;

public class ServerData
{
	public int UserId;
	public string UserNickName;

	public MapList MapinfoSever; // 전체 맵 정보

	//맵 정보
	public Type<MapInfo> NowMap = new Type<MapInfo>(new MapInfo()); // 현재 사용중인 맵 정보
	public Dictionary<Vector2Int, AreaInfo> AreaDic = new Dictionary<Vector2Int, AreaInfo>(); // 각 맵 지점의 정보 리스트
	public List<AreaInfo> Codinate = new List<AreaInfo>(); // 꼭짓점

	//적 정보
	public Dictionary<int, RoundEnemyInfo> EnemyInfo = new Dictionary<int, RoundEnemyInfo>();
}
