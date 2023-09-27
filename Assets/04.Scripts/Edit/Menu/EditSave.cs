using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditSave : EditMenuBase
{
	public TMP_InputField MapNameInput;
	public CaptureImg Capture;

	public Button SaveBtn;

	protected override void Awake()
	{
		base.Awake();

		SaveBtn.onClick.AddListener(SetSave);
		Socket.ins.SocketEventDic.Add("Add_Map", (str) => AddMapCheck());
	}

	private void SetSave()
	{
		if (MapNameInput.text == "")
			return;

		StartCoroutine(SaveFunction());
	}

	private IEnumerator SaveFunction()
	{
		var sendData = new MapInfo_Send();
		sendData.codinate = JsonUtility.ToJson(editManager.EditNode);
		sendData.enemyInfo = JsonUtility.ToJson(editManager.EnemyList);
		MapInfoList saveMapinfo = new MapInfoList();
		foreach (var info in MainGameData.s_serverData.AreaDic.Values)
		{
			var infosave = new MapAreaInfoSave();
			infosave.NodeNum = info.NodeNum;
			infosave.NotMove = info.Notmove;

			saveMapinfo.InfoList.Add(infosave);
		}
		sendData.movelist = JsonUtility.ToJson(saveMapinfo);
		sendData.userId = MainGameData.s_serverData.UserId;
		sendData.mapName = MapNameInput.text;

		yield return StartCoroutine(Capture.CaptureCam((value) => sendData.mapImg = value));

		var sendJson = JsonUtility.ToJson(sendData);

		Socket.ins.ws_SendMessage("Add_Map/" + sendJson);


	}

	private void AddMapCheck()
	{
		GameManager.ins.GetMapinfo();
		MainGameData.s_progressEdit.SetValue(EditProgrss.main);
		MainGameData.s_progressMainGame.SetValue(GameProgress.EditSelect);
	}

}
