public static class MainGameData
{
	//Data
	public static ServerData s_serverData = new ServerData(); // �������� �޾ƿ��� ����
	public static ClientData s_clientData = new ClientData(); // Ŭ�󿡼� �޾ƿ��� ����
	public static GameData s_gameData = new GameData();       // ���� ������ ����

	//progress
	public static Type<GameProgress> s_progressMainGame = new Type<GameProgress>(GameProgress.Lobby); // ��ü ����
	public static Type<LoginProgress> s_progressLogin = new Type<LoginProgress>(LoginProgress.selectLogin);  // �α��� ����
	public static Type<EditProgrss> s_progressEdit = new Type<EditProgrss>(EditProgrss.main);         // ������� ����

	//Click
	public static SortDicAction<AreaInfo> GameTouchMap = new SortDicAction<AreaInfo>("GameTouchMap"); // ���Ӹ���϶� Ŭ���� ����
	public static SortDicAction<AreaInfo> EditTouchMap = new SortDicAction<AreaInfo>("EditTouchMap"); // ��������϶� Ŭ���� ����
}
