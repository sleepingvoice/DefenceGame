using System.Collections.Generic;
using UnityEngine;
using Gu;

public class MapData
{
    public Dictionary<Vector2Int, MapAreaInfo> PointList = new Dictionary<Vector2Int, MapAreaInfo>();
    public float AreawidthLength = 0f;
    public float AreaheigthLength = 0f;
    public int Width = 0;
    public int Hegith = 0;

    public Type<MapAreaInfo> TouchMap = new Type<MapAreaInfo>();
    public NotMovePoint NotMoveList = new NotMovePoint();
    public List<MapAreaInfo> MoveList = new List<MapAreaInfo>();
    public List<GameObject> CanBuildObj = new List<GameObject>();
    public List<GameObject> NotBuildObj = new List<GameObject>();
}
