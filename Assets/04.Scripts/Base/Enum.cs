using System;

[Serializable]
public enum ChessRank
{
	None, Pawn, Knight, Bishop, Rook, Queen, King
}

public enum GameProgress
{
	Login, Lobby, GamePlay, End, EditSelect, Edit
}


public enum LoginProgress
{
	selectLogin,nickNameSelect,finish
}

public enum EditProgrss
{
	main, build, destination, enemy, save
}