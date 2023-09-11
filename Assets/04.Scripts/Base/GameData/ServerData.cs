using System.Collections.Generic;
using UnityEngine;

public class ServerData
{
    public int UserId;
    public string UserName; 

    public MapList MapinfoSever; // ��ü �� ����

    //�� ����
    public Type<MapInfo> NowMap = new Type<MapInfo>(new MapInfo()); // ���� ������� �� ����
    public Dictionary<Vector2Int, AreaInfo> AreaDic = new Dictionary<Vector2Int, AreaInfo>(); // �� �� ������ ���� ����Ʈ
    public List<AreaInfo> Codinate = new List<AreaInfo>(); // ������

    //�� ����
    public Dictionary<int, RoundEnemyInfo> EnemyInfo = new Dictionary<int, RoundEnemyInfo>();
}
