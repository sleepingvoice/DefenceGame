using UnityEngine;
using UnityEngine.UI;

public class UIManager_EditSelect : MonoBehaviour
{
	[Header("SelectMenu")]
	public Button SelectMapBtn;
	public Button MapaEditBtn;
	public Button BackBtn;

	[Header("MapSelect")]
	public GameObject MapSelectWindow;

	private void Awake()
	{
		MainGameData.s_progressMainGame.AddListener(Active);

		SelectMapBtn.onClick.AddListener(InitWindow);
		MapaEditBtn.onClick.AddListener(() => MainGameData.s_progressMainGame.SetValue(GameProgress.Edit));
		BackBtn.onClick.AddListener(() => MainGameData.s_progressMainGame.SetValue(GameProgress.Lobby));
	}

	private void Active(GameProgress progress)
	{
		this.gameObject.SetActive(progress == GameProgress.EditSelect);
		MapSelectWindow.SetActive(false);
	}

	private void InitWindow()
	{
		MapSelectWindow.SetActive(true);
		var window = MapSelectWindow.GetComponent<EditMapWindow>();
		window.InitBar();
		window.StartGameBtn.onClick.RemoveAllListeners();
		window.StartGameBtn.onClick.AddListener(StartGame);
	}

	private void StartGame()
	{
	}
}
