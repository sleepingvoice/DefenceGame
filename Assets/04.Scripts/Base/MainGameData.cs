using System.Collections.Generic;

public static class MainGameData
{
    public static bool EditMode = false;

    public static MapData MapInfo = new MapData();
    public static EnemyData EnemyInfo = new EnemyData();
    public static NextRankList NextRankList = new NextRankList();

    public static Dictionary<ChessRank,TowerState> TowerState = new Dictionary<ChessRank, TowerState>();

    public static Dictionary<ChessRank,KeyValuePair<int, int>> BulletList = new Dictionary<ChessRank, KeyValuePair<int, int>>();
    public static Type<int> Money = new Type<int>(500);
    public static Type<int> EnemyNum = new Type<int>(0);
    public static Type<int> RoundTime = new Type<int>(0);

    public static Type<bool> BuildUi = new Type<bool>(false);

    public static Type<GameProgress> ProgressValue = new Type<GameProgress>(GameProgress.Lobby);
    public static Type<int> NowRound = new Type<int>(0);
    public static GameRule MainGameRule = new GameRule();
}
