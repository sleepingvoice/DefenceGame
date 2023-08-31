using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    public Type<List<EnemyInfo>> EnemyList = new Type<List<EnemyInfo>>();
    public List<AreaInfo> TargetList = new List<AreaInfo>();
    public Dictionary<int, RoundEnemyInfo> EnemyInfo = new Dictionary<int, RoundEnemyInfo>();
}
