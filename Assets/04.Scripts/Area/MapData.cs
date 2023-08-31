using System.Collections.Generic;
using UnityEngine;
using Gu;

public class MapData
{
    public MapList GetMapInfo; // ��ü �� ����
    public Type<MapInfo> NowMap = new Type<MapInfo>(new MapInfo()); // ���� ������� �� ����

    public Dictionary<Vector2Int, AreaInfo> AreaList = new Dictionary<Vector2Int, AreaInfo>(); // �� ������ ���� ����Ʈ
    public Dictionary<Vector2Int, AreaInfo> CodinateDic = new Dictionary<Vector2Int, AreaInfo>(); // ������

    public float AreawidthLength = 0f;
    public float AreaheigthLength = 0f;
    public int Width = 0;
    public int Hegith = 0;

    public SortDicAction<AreaInfo> GameTouchMap = new SortDicAction<AreaInfo>("GameTouchMap"); // ���Ӹ���϶� Ŭ���� ����
    public SortDicAction<AreaInfo> EidtTouchMap = new SortDicAction<AreaInfo>("EditTouchMap"); // ��������϶� Ŭ���� ����

    public List<GameObject> CanBuildObj = new List<GameObject>();
    public List<GameObject> NotBuildObj = new List<GameObject>();
}
