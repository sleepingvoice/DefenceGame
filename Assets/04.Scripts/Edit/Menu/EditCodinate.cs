using System.Collections.Generic;
using Gu;
using UnityEngine;
using UnityEngine.UI;

public class EditCodinate : EditMenuBase
{
	[Header("Function")]
	public List<Color> TargetColor;
	public Button AddBtn;
	public Button RemoveBtn;
	public ObjPool MenuPool;
	public Button SaveBtn;

	private int _targetNum = 0;
	private List<Material> _codinateMatList = new List<Material>();
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
			TmpObj.TargetInfo.OutLineObj.SetActive(false);
			MenuPool.DisableObject(TmpObj.gameObject);
			_menuList.RemoveAt(_menuList.Count - 1);
		});

		MainGameData.s_mapData.EditTouchMap.InsertDic(ClickMap);
		SaveBtn.onClick.AddListener(Save);
	}

	private void OnEnable()
	{
		CheckFinish = false;
		if (editManager != null)
			editManager.EditNode = new CodinateList();
	}

	private void OnDisable()
	{
		if (CheckFinish)
			return;
		foreach(var info in MainGameData.s_mapData.AreaDic.Values)
		{
			if (info.OutLineObj.GetComponent<MeshRenderer>().material == editManager.AreaMat)
				continue;

			info.OutLineObj.GetComponent<MeshRenderer>().material = editManager.AreaMat;
			info.OutLineObj.SetActive(false);
		}
	}

	private void AddMenu()
	{
		if (_menuList.Count >= _codinateMatList.Count)
			return;

		var Tmpobj = MenuPool.GetObject().GetComponent<EditCodinateMenu>();

		_menuList.Add(Tmpobj);
		Tmpobj.CodinateNum = _menuList.Count;
		Tmpobj.SelectCheck(false);
		Color TmpColor = TargetColor[_menuList.Count - 1];
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
		if (MainGameData.s_editProgress.Value == EditProgrss.destination)
		{
			if (_targetNum <= 0 && !info.Notmove)
				return;

			if (_nowTarget.OutLineObj != null)
			{
				_nowTarget.OutLineObj.GetComponent<MeshRenderer>().material = editManager.NormalMat;
				_nowTarget.OutLineObj.SetActive(false);
			}
			info.OutLineObj.GetComponent<MeshRenderer>().material = _codinateMatList[_targetNum - 1];
			info.OutLineObj.SetActive(true);
			_menuList[_targetNum-1].TargetInfo = info;
			_nowTarget = info;
		}
	}

	private void Save()
	{
		var temList = new CodinateList();

		for (int i=0;i<_menuList.Count;i++)
		{
			if (_menuList[i].TargetInfo.OutLineObj.activeSelf)
			{
				temList.NodeList.Add(_menuList[i].TargetInfo.NodeNum);
			}
		}

		List<Vector2Int> temp = temList.NodeList;

		for (int i = 0; i < temp.Count-1; i++)
		{
			if (GameManager.ins.Check.PathFindingAstar(MainGameData.s_mapData.AreaDic[temp[i]], MainGameData.s_mapData.AreaDic[temp[i + 1]]).Count == 0)
			{
				Debug.Log(i);
				Debug.LogError("길이 없습니다.");
				return;
			}
		}

		editManager.EditNode = temList;
		CheckFinish = true;
		MainGameData.s_editProgress.SetValue(EditProgrss.main);
	}

	protected override void Start()
	{
		base.Start();

		for (int i = 0; i < TargetColor.Count; i++) {
			var tmpMat = Instantiate(editManager.AreaMat);
			tmpMat.SetColor("_OutlineColor",TargetColor[i]);
			_codinateMatList.Add(tmpMat);
		}
	}
}
