using System;
using System.Collections.Generic;
using Gu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickCheck : MonoBehaviour
{
	public Camera MainCam;
	public bool ClickOk;
	public EditBuild CheckEdit;

	private EventSystem _system;
	private Action _checkClick = null;
	private ServerData _mapInfo = MainGameData.s_serverData;

	private void Awake()
	{
		MainGameData.s_progressMainGame.AddListener(SetClickType);
	}

	#region 맵 클릭 체크
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

		if (InputAll() != Vector3.zero)
		{
			Ray ray = MainCam.ScreenPointToRay(InputAll());
			if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 6))
			{
				Vector2Int MapNum = new Vector2Int((int)((int)(hit.point.x + GameManager.ins.AreaManager.AreaSize.x / 2) / MainGameData.s_clientData.AreaWidthLength), (int)((int)(hit.point.z + GameManager.ins.AreaManager.AreaSize.z / 2) / MainGameData.s_clientData.AreaHeigthLength));
				MapNum = new Vector2Int(Math.Clamp(MapNum.x, 0, MainGameData.s_clientData.Width - 1), Math.Clamp(MapNum.y, 0, MainGameData.s_clientData.Hegith - 1));

				MainGameData.GameTouchMap.SetValue(_mapInfo.AreaDic[MapNum]);
			}
		}
	}

	private AreaInfo beforehit;

	private void EditPlayTouchCheck()
	{

		if (InputAll() != Vector3.zero)
		{
			Ray ray = MainCam.ScreenPointToRay(InputAll());
			if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 6))
			{
				Vector2Int MapNum = new Vector2Int((int)((int)(hit.point.x + GameManager.ins.AreaManager.AreaSize.x / 2) / MainGameData.s_clientData.AreaWidthLength), (int)((int)(hit.point.z + GameManager.ins.AreaManager.AreaSize.z / 2) / MainGameData.s_clientData.AreaHeigthLength));
				MapNum = new Vector2Int(Math.Clamp(MapNum.x, 0, MainGameData.s_clientData.Width - 1), Math.Clamp(MapNum.y, 0, MainGameData.s_clientData.Hegith - 1));

				if (beforehit == _mapInfo.AreaDic[MapNum])
					return;

				if (!CheckEdit.TouchOut)
				{
					CheckEdit.TouchOut = true;
					CheckEdit.CheckClick = _mapInfo.AreaDic[MapNum].OutLineObj.activeSelf ? false : true;
				}

				beforehit = _mapInfo.AreaDic[MapNum];
				MainGameData.EditTouchMap.SetValue(_mapInfo.AreaDic[MapNum]);
			}
		}
		else
		{
			if (CheckEdit.TouchOut)
				CheckEdit.TouchOut = false;
		}
	}

	#endregion

	#region Input

	private Vector3 InputAll()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		if (Input.GetMouseButton(0))
		{
			return Input.mousePosition;
		}
		return Vector3.zero;
#else
		if (Input.touchCount != 0)
		{
			int Count = Input.touchCount;
			Queue<int> NoTouchQueue = utility.CheckTouchNum(Count);
			if (NoTouchQueue.Count != 0)
			{
				Debug.Log("터치");
				return Input.GetTouch(NoTouchQueue.Dequeue()).position;
			}
		}
		return Vector3.zero;
#endif
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
