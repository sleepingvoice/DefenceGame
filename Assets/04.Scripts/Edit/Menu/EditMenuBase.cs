using Gu;
using UnityEngine;
using UnityEngine.UI;

public class EditMenuBase : MonoBehaviour
{
	[Header("Active")]
	public EditProgrss ActiveProgress;
	public Button ClickBtn;

	protected ShowMap _areaShow;
	protected UIManager_Edit _editManager;

	protected virtual void Awake()
	{
		ClickBtn.onClick.AddListener(() => MainGameData.s_progressEdit.SetValue(ActiveProgress));
		MainGameData.s_progressEdit.AddListener((progress) => this.gameObject.SetActive(progress == ActiveProgress));
	}

	protected virtual void Start()
	{
		_areaShow = GameManager.ins.AreaManager.GetComponent<ShowMap>();
		_editManager = GameManager.ins.UI_Edit;
	}
}
