using UnityEngine;
using UnityEngine.UI;

public class EditBuild : EditMenuBase
{
	[Header("Function")]
	public Button CorrectBtn;

	[HideInInspector] public bool CheckClick = false;
	[HideInInspector] public bool TouchOut = false;

	private ServerData _mapData = MainGameData.s_serverData;

	protected override void Awake()
	{
		base.Awake();
		MainGameData.EditTouchMap.InsertDic(BuildClick);
		CorrectBtn.onClick.AddListener(() => MainGameData.s_progressEdit.SetValue(EditProgrss.main));
	}

	protected override void Start()
	{
		base.Start();
	}

	private void BuildClick(AreaInfo targetarea)
	{
		if (MainGameData.s_progressEdit.Value == EditProgrss.build)
		{
			if (targetarea.Notmove == CheckClick) // 클릭중인 상태와 move상태가 다를때 실행
				return;

			targetarea.OutLineObj.GetComponent<MeshRenderer>().material = CheckClick ? _areaShow.BuildMat : _areaShow.NormalMat;

			if (_mapData.AreaDic.ContainsKey(targetarea.NodeNum))
			{
				_mapData.AreaDic[targetarea.NodeNum].Notmove = CheckClick;
			}
		}
	}
}
