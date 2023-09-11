using System;
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

	#region �� Ŭ�� üũ
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
				Vector2Int MapNum = new Vector2Int((int)((int)(hit.point.x + GameManager.ins.AreaManager.AreaSize.x / 2) / MainGameData.s_clientData.AreaWidthLength), (int)((int)(hit.point.z + GameManager.ins.AreaManager.AreaSize.z / 2) / MainGameData.s_clientData.AreaHeigthLength));
				MapNum = new Vector2Int(Math.Clamp(MapNum.x, 0, MainGameData.s_clientData.Width- 1), Math.Clamp(MapNum.y, 0, MainGameData.s_clientData.Hegith - 1));

				MainGameData.GameTouchMap.SetValue(_mapInfo.AreaDic[MapNum]);
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
				Vector2Int MapNum = new Vector2Int((int)((int)(hit.point.x + GameManager.ins.AreaManager.AreaSize.x / 2) / MainGameData.s_clientData.AreaWidthLength), (int)((int)(hit.point.z + GameManager.ins.AreaManager.AreaSize.z / 2) / MainGameData.s_clientData.AreaHeigthLength));
				MapNum = new Vector2Int(Math.Clamp(MapNum.x, 0, MainGameData.s_clientData.Width - 1), Math.Clamp(MapNum.y, 0, MainGameData.s_clientData.Hegith - 1));

				if (beforehit == _mapInfo.AreaDic[MapNum])
					return;

				if(!CheckEdit.CheckClick)
					CheckEdit.CheckClick = _mapInfo.AreaDic[MapNum].OutLineObj.activeSelf ? false : true;

				beforehit = _mapInfo.AreaDic[MapNum];
				MainGameData.EditTouchMap.SetValue(_mapInfo.AreaDic[MapNum]);
			}
		}
		else
		{
			if (!CheckEdit.CheckClick)
				CheckEdit.CheckClick = true;
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
