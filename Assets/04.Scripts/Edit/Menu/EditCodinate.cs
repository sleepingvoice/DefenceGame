using System.Collections.Generic;
using Gu;
using UnityEngine;
using UnityEngine.UI;

public class EditCodinate : EditMenuBase
{
	[Header("Function")]
	public Button AddBtn;
	public Button RemoveBtn;
	public ObjPool MenuPool;
	public Button SaveBtn;

	private int _targetNum = 0;
	private List<EditCodinateMenu> _menuList = new List<EditCodinateMenu>();
	private bool CheckFinish = false;
	private AreaInfo _nowTarget = null;

	protected override void Awake()
	{
		base.Awake();
		AddBtn.onClick.AddListener(AddMenu);

		RemoveBtn.onClick.AddListener(() =>
		{
			var TmpObj = _menuList[_menuList.Count - 1];
			if (TmpObj.TargetInfo.OutLineObj != null)
				TmpObj.TargetInfo.OutLineObj.GetComponent<MeshRenderer>().material = _areaShow.AreaMat;
			MenuPool.DisableObject(TmpObj.gameObject);
			_menuList.RemoveAt(_menuList.Count - 1);
		});

		MainGameData.EditTouchMap.InsertDic(ClickMap);
		SaveBtn.onClick.AddListener(Save);
	}

	private void OnEnable()
	{
		CheckFinish = false;
		if (_editManager != null)
			_editManager.EditNode = new CodinateList();
	}

	private void OnDisable()
	{
		if (CheckFinish)
			return;
		foreach (var info in MainGameData.s_serverData.AreaDic.Values)
		{
			if (info.OutLineObj.GetComponent<MeshRenderer>().material == _areaShow.AreaMat)
				continue;

			info.OutLineObj.GetComponent<MeshRenderer>().material = _areaShow.AreaMat;
		}
	}

	private void AddMenu()
	{
		if (_menuList.Count >= _areaShow.CodinateMat.Count)
			return;

		var Tmpobj = MenuPool.GetObject().GetComponent<EditCodinateMenu>();

		_menuList.Add(Tmpobj);
		Tmpobj.CodinateNum = _menuList.Count;
		Tmpobj.SelectCheck(false);
		Color TmpColor = _areaShow.CodinateColor[_menuList.Count - 1];
		TmpColor = new Color(TmpColor.r, TmpColor.g, TmpColor.b, 0.3f);
		Tmpobj.GetComponent<Image>().color = TmpColor;
		Tmpobj.SelectBtn.onClick.AddListener(() =>
		{
			_targetNum = Tmpobj.CodinateNum;

			foreach (var menu in _menuList)
			{
				menu.SelectCheck(false);
			}
			Tmpobj.SelectCheck(true);

			_nowTarget = Tmpobj.TargetInfo;
		});
		Tmpobj.Tex.text = _menuList.Count + "번 위치";
	}

	private void ClickMap(AreaInfo info)
	{
		if (MainGameData.s_progressEdit.Value == EditProgrss.destination)
		{
			if (_targetNum <= 0 && !info.Notmove)
				return;

			if (_nowTarget.OutLineObj != null)
			{
				_nowTarget.OutLineObj.GetComponent<MeshRenderer>().material = _areaShow.NormalMat;
			}
			info.OutLineObj.GetComponent<MeshRenderer>().material = _areaShow.CodinateMat[_targetNum - 1];
			_menuList[_targetNum - 1].TargetInfo = info;
			_nowTarget = info;
		}
	}

	private void Save()
	{
		var temList = new CodinateList();

		for (int i = 0; i < _menuList.Count; i++)
		{
			if (_nowTarget.OutLineObj.GetComponent<MeshRenderer>().material != _areaShow.NormalMat)
			{
				temList.NodeList.Add(_menuList[i].TargetInfo.NodeNum);
			}
		}

		List<Vector2Int> temp = temList.NodeList;

		for (int i = 0; i < temp.Count - 1; i++)
		{
			if (GameManager.ins.Check.PathFindingAstar(MainGameData.s_serverData.AreaDic[temp[i]], MainGameData.s_serverData.AreaDic[temp[i + 1]]).Count == 0)
			{
				Debug.Log(i);
				Debug.LogError("길이 없습니다.");
				return;
			}
		}

		_editManager.EditNode = temList;
		CheckFinish = true;
		MainGameData.s_progressEdit.SetValue(EditProgrss.main);
	}
}
