using System.Collections.Generic;

public static class MainGameData
{
    public static MapData s_mapData = new MapData();

    public static EnemyData s_enemyInfo = new EnemyData();
    public static NextRankList s_nextRankList = new NextRankList();

    public static Dictionary<ChessRank,TowerState> s_towerState = new Dictionary<ChessRank, TowerState>();

    public static Dictionary<ChessRank,KeyValuePair<int, int>> s_bulletList = new Dictionary<ChessRank, KeyValuePair<int, int>>();
    public static Type<int> s_money = new Type<int>(500);
    public static Type<int> s_enemyNum = new Type<int>(0);
    public static Type<int> s_roundTime = new Type<int>(0);

    public static Type<bool> s_buildUi = new Type<bool>(false);

    public static Type<GameProgress> s_progressValue = new Type<GameProgress>(GameProgress.Lobby);
    public static Type<int> s_nowRound = new Type<int>(0);
    public static GameRule s_mainGameRule = new GameRule();

    public static Type<LoginProgress> s_loginProgress = new Type<LoginProgress>(LoginProgress.main);
    public static Type<EditProgrss> s_editProgress = new Type<EditProgrss>(EditProgrss.main);
}
