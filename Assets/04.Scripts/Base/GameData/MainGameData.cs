public static class MainGameData
{
	//Data
	public static ServerData s_serverData = new ServerData(); // 서버에서 받아오는 정보
	public static ClientData s_clientData = new ClientData(); // 클라에서 받아오는 정보
	public static GameData s_gameData = new GameData();       // 게임 도중의 정보

	//progress
	public static Type<GameProgress> s_progressMainGame = new Type<GameProgress>(GameProgress.Lobby); // 전체 과정
	public static Type<LoginProgress> s_progressLogin = new Type<LoginProgress>(LoginProgress.selectLogin);  // 로그인 과정
	public static Type<EditProgrss> s_progressEdit = new Type<EditProgrss>(EditProgrss.main);         // 에딧기능 과정

	//Click
	public static SortDicAction<AreaInfo> GameTouchMap = new SortDicAction<AreaInfo>("GameTouchMap"); // 게임모드일때 클릭한 지점
	public static SortDicAction<AreaInfo> EditTouchMap = new SortDicAction<AreaInfo>("EditTouchMap"); // 에딧모드일때 클릭한 지점
}
