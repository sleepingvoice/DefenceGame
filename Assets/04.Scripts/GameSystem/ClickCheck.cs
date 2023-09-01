using System;
using System.Collections;
using System.Collections.Generic;
using Gu;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCheck : MonoBehaviour
{
	public Camera MainCam;
	public bool ClickOk;

	private EventSystem _system;
	private Action _checkClick = null;
	private MapData _mapInfo = MainGameData.s_mapData;

	private void Awake()
	{
		MainGameData.s_progressValue.AddListener(SetClickType);
	}

	#region ¸Ê Å¬¸¯ Ã¼Å©
	private void SetClickType(GameProgress progress)
	{
		switch (progress)
		{
			case GameProgress.GamePlay:
				_checkClick = GamePlayTouchCheck;
				break;
			case GameProgress.Edit:
				_checkClick = EditPlayTouchCheck;
				break;
			case GameProgress.Login:
				_checkClick = LoginUiInput;
				break;
			default:
				_checkClick = null;
				break;
		}
	}

	private void GamePlayTouchCheck()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 6))
			{
				Vector2Int MapNum = new Vector2Int((int)((int)(hit.point.x + GameManager.ins.AreaManager.AreaSize.x / 2) / MainGameData.s_mapData.AreawidthLength), (int)((int)(hit.point.z + GameManager.ins.AreaManager.AreaSize.z / 2) / MainGameData.s_mapData.AreaheigthLength));
				MapNum = new Vector2Int(Math.Clamp(MapNum.x, 0, GameManager.ins.AreaManager.width-1), Math.Clamp(MapNum.y, 0, GameManager.ins.AreaManager.height-1));

				_mapInfo.GameTouchMap.SetValue(_mapInfo.AreaDic[MapNum]);
			}
		}
	}

	private AreaInfo beforehit;

	private void EditPlayTouchCheck()
	{
		if (Input.GetMouseButton(0))
		{
			Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 6))
			{
				Vector2Int MapNum = new Vector2Int((int)((int)(hit.point.x + GameManager.ins.AreaManager.AreaSize.x / 2) / MainGameData.s_mapData.AreawidthLength), (int)((int)(hit.point.z + GameManager.ins.AreaManager.AreaSize.z / 2) / MainGameData.s_mapData.AreaheigthLength));
				MapNum = new Vector2Int(Math.Clamp(MapNum.x, 0, GameManager.ins.AreaManager.width-1), Math.Clamp(MapNum.y, 0, GameManager.ins.AreaManager.height-1));

				if (beforehit == _mapInfo.AreaDic[MapNum])
					return;

				if(MainGameData.s_mapData.EditTouchMode == 0)
					MainGameData.s_mapData.EditTouchMode = _mapInfo.AreaDic[MapNum].OutLineObj.activeSelf ? 2 : 1;

				beforehit = _mapInfo.AreaDic[MapNum];
				_mapInfo.EditTouchMap.SetValue(_mapInfo.AreaDic[MapNum]);
			}
		}
		else
		{
			if (MainGameData.s_mapData.EditTouchMode != 0)
				MainGameData.s_mapData.EditTouchMode = 0;
		}
	}

	#endregion

	#region loginUi

	private void LoginUiInput()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Selectable next = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

			if (next != null)
				next.Select();
		}
	}

	#endregion


	void Start()
	{
		_system = EventSystem.current;
	}

	// Update is called once per frame
	void Update()
    {
		if (_checkClick != null && ClickOk)
			_checkClick.Invoke();

	}
}
