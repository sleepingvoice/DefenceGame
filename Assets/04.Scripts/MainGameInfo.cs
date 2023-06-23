using System.Collections.Generic;
public static class MainGameInfo
{
    public static bool EditMode = false;
    public static AreaInfo MapInfo = new AreaInfo();
    public static NextRankList NextRankList = new NextRankList();
    public static Dictionary<ChessRank,TowerState> TowerState = new Dictionary<ChessRank, TowerState>();

    public static Type<int> Money = new Type<int>(500);
    public static Type<int> EnmeyNum = new Type<int>(0);

    public static Type<bool> BuildUi = new Type<bool>(false);
}
