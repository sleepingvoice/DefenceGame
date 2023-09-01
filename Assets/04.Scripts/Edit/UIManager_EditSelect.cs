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
		MainGameData.s_progressValue.AddListener((value) => this.gameObject.SetActive(value == GameProgress.EditSelect));

		SelectMapBtn.onClick.AddListener(() => MapSelectWindow.SetActive(true));
		MapaEditBtn.onClick.AddListener(() => MainGameData.s_progressValue.SetValue(GameProgress.Edit));
		BackBtn.onClick.AddListener(() => MainGameData.s_progressValue.SetValue(GameProgress.Lobby));
	}
}
