using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    public Type<List<EnemyInfo>> EnemyList = new Type<List<EnemyInfo>>();
    public List<MapAreaInfo> TargetList = new List<MapAreaInfo>();
    public Dictionary<int, RoundEnemyInfo> EnemyInfo = new Dictionary<int, RoundEnemyInfo>();
}
