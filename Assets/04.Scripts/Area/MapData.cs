using System.Collections.Generic;
using UnityEngine;
using Gu;

public class MapData
{
    public MapList GetMapInfo; // 전체 맵 정보
    public Type<MapInfo> NowMap = new Type<MapInfo>(new MapInfo()); // 현재 사용중인 맵 정보

    public Dictionary<Vector2Int, AreaInfo> AreaList = new Dictionary<Vector2Int, AreaInfo>(); // 맵 지점의 정보 리스트
    public Dictionary<Vector2Int, AreaInfo> CodinateDic = new Dictionary<Vector2Int, AreaInfo>(); // 꼭짓점

    public float AreawidthLength = 0f;
    public float AreaheigthLength = 0f;
    public int Width = 0;
    public int Hegith = 0;

    public SortDicAction<AreaInfo> GameTouchMap = new SortDicAction<AreaInfo>("GameTouchMap"); // 게임모드일때 클릭한 지점
    public SortDicAction<AreaInfo> EditTouchMap = new SortDicAction<AreaInfo>("EditTouchMap"); // 에딧모드일때 클릭한 지점

    public int EditTouchMode = 0;

    public List<GameObject> CanBuildObj = new List<GameObject>();
    public List<GameObject> NotBuildObj = new List<GameObject>();
}
