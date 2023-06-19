using System.Collections.Generic;
public static class MainGameInfo
{
    public static bool EditMode = false;
    public static AreaInfo MapInfo = new AreaInfo();
    public static NextRankList NextRankList = new NextRankList();
    public static Dictionary<ChessRank,TowerState> TowerState = new Dictionary<ChessRank, TowerState>();
}
