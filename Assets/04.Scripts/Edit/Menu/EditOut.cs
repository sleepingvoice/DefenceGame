public class EditOut : EditMenuBase
{
	protected override void Awake()
	{
		base.Awake();
		ClickBtn.onClick.AddListener(() => MainGameData.s_progressMainGame.SetValue(GameProgress.EditSelect));
	}
}
