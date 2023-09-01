using UnityEngine;
using UnityEngine.UI;

public class EditMenuBase : MonoBehaviour
{
	[Header("Active")]
	public EditProgrss ActiveProgress;
	public Button ClickBtn;

	protected UIManager_Edit editManager;
	protected virtual void Awake()
	{
		ClickBtn.onClick.AddListener(() => MainGameData.s_editProgress.SetValue(ActiveProgress));
		MainGameData.s_editProgress.AddListener((progress) => this.gameObject.SetActive(progress == ActiveProgress));
	}

	protected virtual void Start()
	{
		editManager = GameManager.ins.UI_Edit;
	}
}
