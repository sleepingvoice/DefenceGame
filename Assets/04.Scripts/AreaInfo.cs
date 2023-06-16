using System.Collections.Generic;
using UnityEngine;

public class AreaInfo
{
    public Dictionary<Vector2, MapAreaInfo> PointList = new Dictionary<Vector2, MapAreaInfo>();
    public float AreawidthLength = 0f;
    public float AreaheigthLength = 0f;

    public Type<MapAreaInfo> TouchMap = new Type<MapAreaInfo>();
}
