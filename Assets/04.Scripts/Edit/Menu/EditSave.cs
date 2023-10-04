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
		Socket.ins.SocketEventDic.Add("Add_Map", AddMapCheck);
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
		sendData.codinate = JsonUtility.ToJson(_editManager.EditNode);
		sendData.enemyInfo = JsonUtility.ToJson(_editManager.EnemyList);
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

		var sendJson = JsonUtility.ToJson(sendData);

		Socket.ins.ws_SendMessage("Add_Map/" + sendJson);

		yield return null;
	}

	private void AddMapCheck(string str)
	{
		GameManager.ins.SoketAct.Enqueue(() =>
		{
			GameManager.ins.GetMapinfo();
			MainGameData.s_progressEdit.SetValue(EditProgrss.main);
			MainGameData.s_progressMainGame.SetValue(GameProgress.EditSelect);
		});
	}

}
